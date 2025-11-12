using ItemStatsSystem;
using ItemStatsSystem.Items;
using ItemStatsSystem.Stats;
using System.Collections.Generic;

namespace NestedBag
{
    public class HackedModifiers : ModifierDescriptionCollection
    {
        StatCollection stats;
        SlotCollection slots;

        // rewrite init
        void Awake()
        {
            initialized = true;
            list = new List<ModifierDescription>();
            master = GetComponent<Item>();
            stats = GetComponent<StatCollection>();
            slots = GetComponent<SlotCollection>();

            if (master != null)
            {
                master.onItemTreeChanged += OnUpdateItem;
                master.onDurabilityChanged += OnUpdateItem;
            }
        }

        // rewrite destroy
        void OnDestroy()
        {
            if (master != null)
            {
                master.onItemTreeChanged -= OnUpdateItem;
                master.onDurabilityChanged -= OnUpdateItem;
            }
        }

        // transparent modifiers
        void OnUpdateItem(Item master) => ReapplyModifiersOverride();
        void ReapplyModifiersOverride()
        {
            if (master == null) return;

            // refresh all modifiers
            Clear();
            foreach (var modDesc in GrabModDescFromStats())
                Add(modDesc);

            // apply all
            foreach (ModifierDescription descrip in list) descrip.ReapplyModifier(this);

            // collect character modifier for display
            foreach (var modDesc in GetAllCharacterModifiersFromSlots())
                Add(modDesc);
        }

        // helpers
        IEnumerable<ModifierDescription> GrabModDescFromStats()
        {
            foreach (var stat in stats)
                foreach (var modifier in stat.Modifiers)
                {
                    var modDesc = new ModifierDescription(ModifierTarget.Parent, stat.Key, modifier.Type, modifier.Value, modifier.overrideOrder, modifier.overrideOrderValue);
                    modDesc.display = true;
                    yield return modDesc;
                    Add(modDesc);
                }
        }
        IEnumerable<ModifierDescription> GetAllCharacterModifiersFromSlots()
        {
            if (slots == null) yield break;
            foreach (var slot in slots)
            {
                var item = slot.Content;
                if (item == null) continue;
                var modifiers = item.Modifiers;
                if (modifiers == null) continue;
                else if (modifiers is HackedModifiers recur)
                {
                    foreach (var modDesc in recur.GetAllCharacterModifiersFromSlots()) yield return modDesc;
                }
                else
                {
                    foreach (var modDesc in modifiers)
                    {
                        if (modDesc.target != ModifierTarget.Character) continue;
                        yield return modDesc;
                    }
                }
            }
        }
    }
}
