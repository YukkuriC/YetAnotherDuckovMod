using Duckov.Rules.UI;
using HarmonyLib;
using UnityEngine;

namespace YukkuriC.UITweaks.Patches
{
    [HarmonyPatch]
    public static class PatchDifficultySelection
    {
        [HarmonyPrefix, HarmonyPatch(typeof(DifficultySelection), "CheckUnlocked")]
        static bool NoDifficultyLimit(ref bool __result)
        {
            __result = true;
            return false;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(DifficultySelection), nameof(DifficultySelection.CustomDifficultyMarker), MethodType.Setter)]
        static void NoDifficultyLimit_NoMarker(ref bool value)
        {
            value = false;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(DifficultySelection), nameof(DifficultySelection.Execute))]
        static void NoDifficultyLimit_NoMarker2(GameObject ___achievementDisabledIndicator, GameObject ___selectedCustomDifficultyBefore)
        {
            ___achievementDisabledIndicator.transform.position
                = ___selectedCustomDifficultyBefore.transform.position
                = new Vector3(114, 514, 1919);
            DifficultySelection.CustomDifficultyMarker = false;
        }
    }
}
