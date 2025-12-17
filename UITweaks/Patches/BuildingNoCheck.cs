using Duckov.Buildings.UI;
using HarmonyLib;

namespace YukkuriC.UITweaks.Patches
{
    [HarmonyPatch]
    public static class BuildingNoCheck
    {
        [HarmonyPrefix, HarmonyPatch(typeof(BuilderView), "IsValidPlacement")]
        static bool AlwaysValid(ref bool __result)
        {
            __result = true;
            return false;
        }
    }
}
