using Duckov.ItemBuilders;
using Duckov.Utilities;
using ItemStatsSystem;
using ItemStatsSystem.Items;

namespace YukkuriC.Ext
{
    public static class ItemBuilderExt
    {
        public static ItemBuilder ApplyOverride(this ItemBuilder builder, Item item)
        {
            item.CreateSlotsComponent();
            item.CreateStatsComponent();
            item.CreateInventoryComponent();
            item.CreateModifiersComponent();
            foreach (var desc in builder.slots.Values)
            {
                var slot = new Slot(desc.key);
                slot.requireTags.AddRange(desc.requireTags);
                slot.excludeTags.AddRange(desc.excludeTags);
                item.Slots.Add(slot);
            }
            foreach (var desc in builder.stats.Values)
                item.Stats.Add(new Stat(desc.key, desc.value, desc.display));
            foreach (var constant in builder.constants)
                item.Constants.Add(new CustomData(constant));
            foreach (var variable in builder.variables)
                item.Variables.Add(new CustomData(variable));
            foreach (var modifier in builder.modifiers)
                item.Modifiers.Add(modifier);
            item.Modifiers.ReapplyModifiers();
            return builder;
        }
        public static Item ApplyOverride(this Item item, ItemBuilder builder)
        {
            builder.ApplyOverride(item);
            return item;
        }
    }
}
