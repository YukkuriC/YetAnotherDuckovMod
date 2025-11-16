using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace YukkuriC.UITweaks.Patches
{
    [HarmonyPatch]
    public static class PatchSlider
    {
        [HarmonyPrefix, HarmonyPatch(typeof(CustomFaceSlider), "SetValue")]
        static void NoClamp(float value, Slider ___slider)
        {
            ___slider.minValue = Mathf.Min(___slider.minValue, value);
            ___slider.maxValue = Mathf.Max(___slider.maxValue, value);
        }
        [HarmonyPrefix, HarmonyPatch(typeof(CustomFaceSlider), "OnEndEditField")]
        static void NoClamp2(string str, Slider ___slider)
        {
            if (float.TryParse(str, out var value))
            {
                NoClamp(value, ___slider);
            }
        }
    }
}
