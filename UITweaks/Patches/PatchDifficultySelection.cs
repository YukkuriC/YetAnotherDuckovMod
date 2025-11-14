using Duckov.Rules.UI;
using HarmonyLib;

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
    }
}
