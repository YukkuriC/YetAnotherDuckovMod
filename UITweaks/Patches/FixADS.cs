using HarmonyLib;

namespace YukkuriC.UITweaks.Patches
{
    [HarmonyPatch]
    public static class FixADS
    {
        [HarmonyPostfix, HarmonyPatch(typeof(ItemAgent_Gun), "UpdateAds")]
        static void FixADSNaN(ref float ___adsValue)
        {
            if (float.IsNaN(___adsValue)) ___adsValue = 0;
        }
    }
}
