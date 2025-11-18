using HarmonyLib;
using ProjectileReflector.Skills;
using UnityEngine;

namespace ProjectileReflector
{
    using static ModConfigs;

    [HarmonyPatch]
    public static class ReflectController
    {
        public const float MELEE_RANGE_BASE = 1.5f;

        #region checks
        public static bool InReflectRange(Transform self, CharacterMainControl player, Status curStatus, out float range)
        {
            var delta = self.position - player.transform.position;
            delta.y = 0;
            range = curStatus == Status.ACTIVE ? REFLECT_RANGE : REFLECT_RANGE_PASSIVE;
            if (AUTO_SCALE_MELEE_RANGE)
            {
                var melee = player.GetMeleeWeapon();
                if (melee != null) range *= melee.AttackRange / MELEE_RANGE_BASE;
            }

            return delta.sqrMagnitude < range * range
                   && (
                       IGNORES_ANGLE
                       || Vector3.Angle(delta, player.CurrentAimDirection) <= 90
                   );
        }
        #endregion

        #region body
        public enum Status
        {
            IDLE,
            PASSIVE,
            ACTIVE
        }

        private static float statusUntil = -114514;
        private static Status curStatus = Status.IDLE;

        public static bool DoReflectGrenade = false; // 就不get set了，怪矫情的

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
            if (!ENABLE_ACTIVE_REFLECT || !__result || !self.IsMainCharacter) return;
            if (__result) DoReflectGrenade = true;
            if (PlayerStatus == Status.PASSIVE) return;
            //Debug.Log("swing extend");
            ExtendStatus(Status.ACTIVE, TIME_SWING_ACTIVE);

            // fly blade
            if (ENABLES_FLYING_BLADE && hasRunInput && !self.movementControl.Moving)
            {
                FlyBlade.CreateFlyBlade(self);
                self.UseStamina(self.MaxStamina / 3);
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
