using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using static YukkuriC.Event.ItemEvents;

namespace YukkuriC.Item
{
    [HarmonyPatch]
    static class ItemHooks
    {
        [HarmonyPrefix, HarmonyPatch(typeof(ItemAgent_Gun), "TransToFire")]
        static bool PreShoot(ItemAgent_Gun __instance)
        {
            var self = __instance;
            var ev = new GunShotEvent(__instance.Holder, __instance);
            OnPreShootGun?.Invoke(ev);
            return !ev.cancelled;
        }
    }
}
