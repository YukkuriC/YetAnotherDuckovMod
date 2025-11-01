using HarmonyLib;
using UnityEngine;

namespace ProjectileReflector
{
    using static ModConfigs;

    [HarmonyPatch]
    public static class ReflectController
    {
        #region body
        public enum Status
        {
            IDLE,
            PASSIVE,
            ACTIVE
        }

        private static float statusUntil = -114514;
        private static Status curStatus = Status.IDLE;

        public static Status PlayerStatus
        {
            get
            {
                if (Time.time > statusUntil) return Status.IDLE;
                return curStatus;
            }
        }

        public static void ExtendStatus(Status newStat, float duration)
        {
            statusUntil = Time.time + duration;
            curStatus = newStat;
            //Debug.Log($"extend status {newStat} to {statusUntil}, now is {PlayerStatus}");
        }
        #endregion


        #region hooks
        [HarmonyPostfix, HarmonyPatch(typeof(CharacterMainControl), nameof(CharacterMainControl.Attack))]
        static void PostAttack(CharacterMainControl __instance, bool __result)
        {
            if (!__result || !__instance.IsMainCharacter || PlayerStatus == Status.PASSIVE) return;
            //Debug.Log("swing extend");
            ExtendStatus(Status.ACTIVE, TIME_SWING_ACTIVE);
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(InputManager), nameof(InputManager.SetAimType))]
        //static void PostAim(AimTypes aimType)
        //{ failed
        //}
        #endregion
    }
}
