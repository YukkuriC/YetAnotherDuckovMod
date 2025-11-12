using Duckov.Utilities;
using ItemStatsSystem;
using ItemStatsSystem.Items;
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
        };

        void OnEnable()
        {
            var bag = ItemAssetsCollection.GetPrefab(BAG_ID);
            var go = bag.gameObject;

            // slots: create new slots
            var slots = bag.slots;
            if (slots == null) slots = bag.slots = go.AddComponent<SlotCollection>();
            else slots.Clear();
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
                bag.Tags.Add(tag);
            }
        }
        void OnDisable()
        {
            // give up
        }
    }
}
