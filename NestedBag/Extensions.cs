using ItemStatsSystem;
using ItemStatsSystem.Stats;
using System;
using System.Collections.Generic;
using System.Text;

namespace NestedBag
{
    public static class Extensions
    {
        public static V GetValueOrDefault<K, V>(this IDictionary<K, V> dict, K key, Func<V> getter)
        {
            if (dict.ContainsKey(key)) return dict[key];
            return dict[key] = getter();
        }

        public static ModifierDescription Copy(this ModifierDescription self)
        {
            return new ModifierDescription(self.target, self.key, self.type, self.value, self.overrideOrder, self.order)
            {
                display = self.display
            };
        }

        static readonly Comparison<ModifierDescription> ModDescOrderComparison = (ModifierDescription a, ModifierDescription b) => a.Order - b.Order;
        static readonly Func<List<ModifierDescription>> NewList = () => new List<ModifierDescription>();
        static readonly Func<Dictionary<ModifierType, ModifierDescription>> NewDict = () => new Dictionary<ModifierType, ModifierDescription>();
        public static IEnumerable<ModifierDescription> MergedByStat(this IEnumerable<ModifierDescription> original)
        {
            var groupByType = new Dictionary<string, Dictionary<ModifierType, ModifierDescription>>();
            var overrideOrdersByType = new Dictionary<string, List<ModifierDescription>>();

            // collect all
            foreach (var modDesc in original)
            {
                var independentCalc = modDesc.IsOverrideOrder && modDesc.type != ModifierType.PercentageAdd;
                if (independentCalc)
                {
                    var list = overrideOrdersByType.GetValueOrDefault(modDesc.key, NewList);
                    list.Add(modDesc);
                }
                else
                {
                    var dict = groupByType.GetValueOrDefault(modDesc.key, NewDict);
                    if (dict.ContainsKey(modDesc.type))
                    {
                        if (modDesc.type == ModifierType.PercentageMultiply) dict[modDesc.type].value = (1 + dict[modDesc.type].value) * (1 + modDesc.value) - 1;
                        else dict[modDesc.type].value += modDesc.value;
                    }
                    else
                    {
                        dict[modDesc.type] = modDesc.Copy();
                    }
                }
            }

            // re-add all stats to sorters
            foreach (var kv in groupByType)
            {
                var list = overrideOrdersByType.GetValueOrDefault(kv.Key, NewList);
                foreach (var sub in kv.Value.Values) list.Add(sub);
            }

            // sort and output
            foreach (var list in overrideOrdersByType.Values)
            {
                list.Sort(ModDescOrderComparison);
                foreach (var sub in list) yield return sub;
            }
        }

        public static T SetMaster<T>(this T inst, Item master) where T : ItemComponent
        {
            inst.master = master;
            return inst;
        }
    }
}
