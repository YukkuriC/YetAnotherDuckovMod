using Duckov.Economy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YukkuriC.AlienGuns.Items
{
    public static class FormulaRegistry
    {
        struct MyCraftingFormula
        {
            static readonly string[] COMMON_WORKBENCH = { "WorkBenchAdvanced" };
            public int id;
            public string items;

            public CraftingFormula ToCrafting() => new CraftingFormula
            {
                unlockByDefault = true,
                tags = COMMON_WORKBENCH,
                id = $"YukkuriC.AlienGun.{id}",
                result = new CraftingFormula.ItemEntry { amount = 1, id = id + ItemUtils.ITEM_START_ID },
                cost = new Cost
                {
                    items = (
                        from s in items.Split(",")
                        select FromString(s)
                    ).ToArray()
                },
            };

            public static Cost.ItemEntry FromString(string pattern)
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
        public static void Init()
        {
            CraftingList = (
                from i in "formulas.crafting.json".ToResourceJson<MyCraftingFormula[]>()
                select i.ToCrafting()
            ).ToArray();
        }
        public static void Load()
        {
            CraftingFormulaCollection.Instance.list.AddRange(CraftingList);
        }
        public static void Unload()
        {
            CraftingFormulaCollection.Instance.list.RemoveAll(x => CraftingList.Contains(x));
        }
    }
}
