using Duckov.Modding;
using Duckov.Modding.UI;
using HarmonyLib;
using UnityEngine;

namespace YukkuriC.UITweaks.Patches
{
    [HarmonyPatch]
    public static class PatchModSortingButton
    {
        static bool HasShift() => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        static bool ForceJump(int oldIndex, int newIndex)
        {
            ModManager.Reorder(oldIndex, newIndex);
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(ModEntry), "OnButtonReorderDownClicked")]
        static bool ShiftJumpBottom(int ___index)
        {
            if (!HasShift()) return true;
            return ForceJump(___index, ModManager.modInfos.Count - 1);
        }
        [HarmonyPrefix, HarmonyPatch(typeof(ModEntry), "OnButtonReorderUpClicked")]
        static bool ShiftJumpTop(int ___index)
        {
            if (!HasShift()) return true;
            return ForceJump(___index, 0);
        }
    }
}
