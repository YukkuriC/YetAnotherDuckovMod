using YukkuriC;
using Duckov.Utilities;
using HarmonyLib;
using UnityEngine;

namespace ProjectileReflector
{
    using static ReflectController;
    using static ModConfigs;

    [HarmonyPatch]
    public static class ProjectileReflectorWorker
    {
        #region checks
        static float ReflectBackChance(Status s)
        {
            if (s == Status.ACTIVE) return CHANCE_BACK_ACTIVE;
            return CHANCE_BACK_PASSIVE;
        }

        static bool ShouldCheckReflect(Projectile self, CharacterMainControl player, out float staminaCost)
        {
            staminaCost = 0;
            if (
                self.context.team == Teams.player
                || player == null
                || !(player.CurrentHoldItemAgent is ItemAgent_MeleeWeapon)
            ) return false;

            // type 1: active strike
            if (PlayerStatus == Status.ACTIVE)
            {
                staminaCost = -ACTIVE_STAMINA_GAIN;
                return true;
            }

            // type 2: has extra stamina
            if (!ENABLE_PASSIVE_REFLECT) return false;
            if ( // filter various types
                (PASSIVE_REFLECT_BY_ADS && !player.IsInAdsInput)
                || (!PASSIVE_REFLECT_WHEN_RUNNING && player.Running)
                || (!PASSIVE_REFLECT_WHEN_DASHING && player.Dashing)
               ) return false;
            staminaCost = self.context.damage * PASSIVE_STAMINA_COST;
            return player.CurrentStamina >= staminaCost;
        }
        #endregion

        [HarmonyPrefix, HarmonyPatch(typeof(Projectile), "UpdateMoveAndCheck")]
        public static void UpdateMove(Projectile __instance, ref Vector3 ___velocity, ref Vector3 ___direction)
        {
            var self = __instance;
            var player = LevelManager.Instance.MainCharacter;
            if (!ShouldCheckReflect(self, player, out float staminaCost)) return;
            var curStatus = PlayerStatus;

            // check melee range & angle
            if (!InReflectRange(self.transform, player, curStatus, out var range)) return;

            // do reflect
            DoReflect(player, self, ref ___velocity, ref ___direction, curStatus);
            if (staminaCost > 0) player.UseStamina(staminaCost);
            else if (staminaCost < 0)
            {
                // hack increase stamina
                // use publicizer
                player.currentStamina = Mathf.Min(player.currentStamina - staminaCost, player.MaxStamina);
            }

            // play melee fx
            var isActive = curStatus == Status.ACTIVE;
            if (!isActive) ExtendStatus(Status.PASSIVE, TIME_PASSIVE_EXTEND);
            FxLib.CreateSlash(player.GetMeleeWeapon(), player, range);
            if (curStatus == Status.ACTIVE) ExtendStatus(Status.ACTIVE, TIME_ACTIVE_EXTEND);
            else ExtendStatus(Status.PASSIVE, TIME_PASSIVE_EXTEND);
        }

        private static void DoReflect(CharacterMainControl player, Projectile self,
            ref Vector3 ___velocity, ref Vector3 ___direction,
            Status curStatus)
        {
            var aimBackProb = ReflectBackChance(curStatus);
            var aimBack = Random.value < aimBackProb;

            if (curStatus == Status.ACTIVE) ModAudio.PlaySoundActive();
            else ModAudio.PlaySoundPassive();

            ref var oldContext = ref self.context;
            self.damagedObjects.Clear();
            Vector3 delta;
            if (aimBack) delta = oldContext.fromCharacter.transform.position - self.transform.position;
            else delta = Vector3.Reflect(___velocity, self.transform.position - player.transform.position);
            delta.y = 0;
            oldContext.direction = ___direction = delta.normalized;
            ___velocity = ___direction * oldContext.speed;
            oldContext.team = Teams.player;
            oldContext.fromCharacter = player;

            // hit fx
            var prefab = player.GetMeleeWeapon()?.hitFx ?? GameplayDataSettings.Prefabs.BulletHitObsticleFx;
            var hitFx = Object.Instantiate(prefab, self.transform.position, Quaternion.LookRotation(Random.onUnitSphere, Vector3.up));
            hitFx.transform.localScale = Vector3.one * (0.5f + Random.value * 0.3f);

            // extra dmg
            if (curStatus == Status.ACTIVE)
            {
                oldContext.distance *= DISTANCE_MULT_ACTIVE;
                oldContext.critRate = ACTIVE_CRITICAL ? 1 : 0;
                oldContext.damage *= DAMAGE_MULT_ACTIVE;
                if (ACTIVE_EXPLOSION && oldContext.explosionRange <= 0)
                {
                    oldContext.explosionRange = ACTIVE_EXPLOSION_RANGE;
                    oldContext.explosionDamage = oldContext.damage * ACTIVE_EXPLOSION_DAMAGE_FACTOR;
                }
            }
            else
            {
                oldContext.distance *= DISTANCE_MULT_PASSIVE;
                oldContext.damage *= DAMAGE_MULT_PASSIVE;
            }
        }
    }
}
