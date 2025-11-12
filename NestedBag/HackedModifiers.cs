using ItemStatsSystem;
using System.Collections.Generic;

namespace NestedBag
{
    public class HackedModifiers : ModifierDescriptionCollection
    {
        StatCollection stats;

        // rewrite init
        void Awake()
        {
            initialized = true;
            if (list == null) list = new List<ModifierDescription>();
            if (master == null) master = GetComponent<Item>();
            if (stats == null) stats = GetComponent<StatCollection>();

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
            foreach (var stat in stats)
                foreach (var modifier in stat.Modifiers)
                {
                    var modDesc = new ModifierDescription(ModifierTarget.Parent, stat.Key, modifier.Type, modifier.Value, modifier.overrideOrder, modifier.overrideOrderValue);
                    modDesc.display = true;
                    Add(modDesc);
                }

            // apply all
            foreach (ModifierDescription descrip in list) descrip.ReapplyModifier(this);
        }
    }
}
