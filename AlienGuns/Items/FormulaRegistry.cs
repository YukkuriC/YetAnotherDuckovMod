using Duckov.Economy;
using ItemStatsSystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Items
{
    public static class FormulaRegistry
    {
        struct MyCraftingFormula
        {
            static readonly string[] COMMON_WORKBENCH = { "WorkBenchAdvanced" };
            public int id;
            public string items;
            public int count;

            public CraftingFormula ToCrafting() => new CraftingFormula
            {
                unlockByDefault = true,
                tags = COMMON_WORKBENCH,
                id = $"YukkuriC.AlienGun.{id}",
                result = new CraftingFormula.ItemEntry { amount = Math.Max(count, 1), id = id + ItemUtils.ITEM_START_ID },
                cost = BuildCost(),
            };
            public DecomposeFormula ToDecomposing() => new DecomposeFormula
            {
                item = id + ItemUtils.ITEM_START_ID,
                result = BuildCost(),
                valid = true,
            };
            Cost BuildCost() => new Cost
            {
                items = (
                    from s in items.Split(",")
                    select FromString(s)
                ).ToArray()
            };

            static Cost.ItemEntry FromString(string pattern)
            {
                var parts = pattern.Split('x');
                return new Cost.ItemEntry
                {
                    id = int.Parse(parts[0]),
                    amount = parts.Length > 1 ? int.Parse(parts[1]) : 1
                };
            }
        }

        static CraftingFormula[] CraftingList;
        static DecomposeFormula[] DecomposingList;
        public static void Init()
        {
            CraftingList = (
                from i in "formulas.crafting.json".ToResourceJson<MyCraftingFormula[]>()
                select i.ToCrafting()
            ).ToArray();
            DecomposingList = (
                from i in "formulas.decomposing.json".ToResourceJson<MyCraftingFormula[]>()
                select i.ToDecomposing()
            ).ToArray();

            // auto pricing
            foreach (var c in CraftingList)
            {
                var gun = ItemAssetsCollection.GetPrefab(c.result.id);
                float price = 0;
                foreach (var i in c.cost.items) price += ItemAssetsCollection.GetPrefab(i.id).Value * i.amount;
                gun.Value = (int)(price * 0.9f / c.result.amount);
            }
        }
        public static void Load()
        {
            ClearCache();
            CraftingFormulaCollection.Instance.list.AddRange(CraftingList);
            DecomposeDatabase.Instance.entries = DecomposeDatabase.Instance.entries.Concat(DecomposingList).ToArray();
        }
        public static void Unload()
        {
            ClearCache();
            CraftingFormulaCollection.Instance.list.RemoveAll(x => CraftingList.Contains(x));
            DecomposeDatabase.Instance.entries = (
                from x in DecomposeDatabase.Instance.entries
                where !DecomposingList.Contains(x)
                select x
            ).ToArray();
        }
        static void ClearCache()
        {
            CraftingFormulaCollection.Instance._entries_ReadOnly = null;
            DecomposeDatabase.Instance._dic = null;
        }
    }
}
