using HarmonyLib;
using UnityEngine;
using YukkuriC.Skills;

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
            var self = __instance;
            if (!ENABLE_ACTIVE_REFLECT || !__result || !self.IsMainCharacter ||
                PlayerStatus == Status.PASSIVE) return;
            //Debug.Log("swing extend");
            ExtendStatus(Status.ACTIVE, TIME_SWING_ACTIVE);

            // fly blade
            if (ENABLES_FLYING_BLADE && hasRunInput && !self.Running)
            {
                FlyBlade.CreateFlyBlade(self);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(InputManager), nameof(InputManager.SetAdsInput))]
        static void UpdatePlayerADSInput(bool ads)
        {
            if (!PASSIVE_REFLECT_BY_ADS || !LevelManager.Instance.MainCharacter.IsInAdsInput) return;
            ExtendStatus(Status.ACTIVE, TIME_ADS_ACTIVE);
        }

        static bool hasRunInput = false;
        [HarmonyPostfix, HarmonyPatch(typeof(InputManager), nameof(InputManager.SetRunInput))]
        static void UpdatePlayerRunInput(bool run)
        {
            hasRunInput = run;
        }

        #endregion
    }
}