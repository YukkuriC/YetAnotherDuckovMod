using HarmonyLib;
using ItemStatsSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace YukkuriC.UITweaks.Patches
{
    [HarmonyPatch]
    public static class PatchItemCollection
    {
        [HarmonyPostfix, HarmonyPatch(typeof(ItemAssetsCollection), nameof(ItemAssetsCollection.InstantiateFallbackItem))]
        static void AssignName(Item __result)
        {
            __result.DisplayNameRaw = "what?";
        }
    }
}
