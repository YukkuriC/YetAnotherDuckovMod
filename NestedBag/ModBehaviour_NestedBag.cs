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
            "TecEquip",
            "Totem",
            "Backpack",
            "Gem",
            "ComputerParts_GPU",
            "DontDropOnDeadInSlot",
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
                var tag = ScriptableObject.CreateInstance<Tag>();
                tag.name = tagKey;
                tag.show = true;
                tag.color = new Color(0.9f, 0.75f, 0.5f);
                bag.Tags.Add(tag);
            }

            // lang
            LocalizationManager.OnSetLanguage += FillLang;
            FillLang(LocalizationManager.CurrentLanguage);
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
            foreach (var tagKey in TAGS_ADD)
            {
                var tag = ScriptableObject.CreateInstance<Tag>();
                tag.name = tagKey;
                bag.Tags.Remove(tag);
            }

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
    }
}
