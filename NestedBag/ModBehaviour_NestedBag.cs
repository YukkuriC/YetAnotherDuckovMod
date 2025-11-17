using Duckov.Utilities;
using ItemStatsSystem;
using ItemStatsSystem.Items;
using SodaCraft.Localizations;
using System.Collections.Generic;
using UnityEngine;

namespace NestedBag
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        const int BAG_ID = 1255;
        const int EXTEND_SLOTS_COUNT = 6;
        static readonly string[] TAGS_ADD = new string[] {
            "Accessory",
            "Totem",
            "Backpack",
            "Gem",
            "ComputerParts_GPU",
            "DontDropOnDeadInSlot",
        };
        static readonly string[] TAGS_ADD_HIDE = new string[] {
            // all gun parts
            "Muzzle",
            "Grip",
            "Stock",
            "Scope",
            "TecEquip",
            "Magazine",
            // for muzzles
            "GunType_PST",
            "GunType_AR",
            "GunType_SMG",
            "GunType_SNP",
            "GunType_SHT",
            "GunType_MAG",
        };
        static readonly HashSet<string> TAGS_WITH_DESCRIP = new HashSet<string>()
        {
            "ComputerParts_GPU",
            "DontDropOnDeadInSlot",
        };
        static readonly Dictionary<string, Color> TAG_COLORS = new Dictionary<string, Color>()
        {
            ["Gem"] = new Color(0.127f, 0.527f, 1),
            ["ComputerParts_GPU"] = new Color(1, 0.597f, 0.42f),
        };
        static Slot oldSlot;

        void OnEnable()
        {
            var bag = ItemAssetsCollection.GetPrefab(BAG_ID);
            var go = bag.gameObject;

            // slots: create new slots
            var slots = bag.slots;
            if (slots == null) slots = bag.slots = go.AddComponent<SlotCollection>();
            else
            {
                if (slots.Count > 0) oldSlot = slots[0];
                slots.Clear();
            }
            for (int i = 0; i < EXTEND_SLOTS_COUNT; i++)
            {
                slots.Add(new Slot($"bag_{i}"));
            }

            // stats: exist is ok
            var stats = bag.stats;
            if (stats == null)
            {
                stats = bag.stats = go.AddComponent<StatCollection>();
                stats.list = new List<Stat>();
            }

            // modifier: use hacked one
            if (bag.modifiers) Destroy(bag.modifiers);
            bag.modifiers = go.AddComponent<HackedModifiers>();

            // tags
            foreach (var tagKey in TAGS_ADD)
            {
                var tag = CreateTagWithKey(tagKey);
                tag.show = true;
                tag.color = TAG_COLORS.GetValueOrDefault(tagKey, Color.black);
                tag.showDescription = TAGS_WITH_DESCRIP.Contains(tagKey);
                bag.Tags.Add(tag);
            }
            foreach (var tagKey in TAGS_ADD_HIDE) bag.Tags.Add(CreateTagWithKey(tagKey));

            // lang
            LocalizationManager.OnSetLanguage += FillLang;
            FillLang(LocalizationManager.CurrentLanguage);

            // 顺手帮官方修个bug，不用谢:3
            var totemHunger = ItemAssetsCollection.GetPrefab(371);
            totemHunger.modifiers.list[1].target = ModifierTarget.Character;
        }
        void OnDisable()
        {
            var bag = ItemAssetsCollection.GetPrefab(BAG_ID);
            var go = bag.gameObject;

            // simple recovers
            bag.slots.Clear();
            if (oldSlot != null) bag.slots.Add(oldSlot);
            Destroy(bag.stats);
            Destroy(bag.modifiers);
            bag.stats = null;
            bag.modifiers = null;
            foreach (var tagKey in TAGS_ADD) bag.Tags.Remove(CreateTagWithKey(tagKey));
            foreach (var tagKey in TAGS_ADD_HIDE) bag.Tags.Remove(CreateTagWithKey(tagKey));

            // lang
            LocalizationManager.OnSetLanguage -= FillLang;
        }

        void FillLang(SystemLanguage lang)
        {
            switch (lang)
            {
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                    LocalizationManager.SetOverrideText("Stat_Performance", "算力");
                    break;
                default:
                    LocalizationManager.SetOverrideText("Stat_Performance", "Performance");
                    break;
            }
        }
        Tag CreateTagWithKey(string tagKey)
        {
            var tag = ScriptableObject.CreateInstance<Tag>();
            tag.name = tagKey;
            return tag;
        }
    }
}
