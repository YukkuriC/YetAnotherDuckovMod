﻿using HarmonyLib;
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
                || player.Dashing // until a better plan
                || !(player.CurrentHoldItemAgent is ItemAgent_MeleeWeapon)
            ) return false;

            // type 1: active strike
            if (PlayerStatus == Status.ACTIVE) return true;

            // type 2: has extra stamina
            staminaCost = self.context.damage * PASSIVE_STAMINA_COST;
            return player.CurrentStamina >= staminaCost;
        }

        static bool InReflectRange(Projectile self, CharacterMainControl player, Status curStatus)
        {
            var delta = self.transform.position - player.transform.position;
            delta.y = 0;
            var rangeSqr = curStatus == Status.ACTIVE ? REFLECT_RANGE_SQR : REFLECT_RANGE_SQR_PASSIVE;
            return delta.sqrMagnitude < rangeSqr
            && Vector3.Angle(delta, player.CurrentAimDirection) <= 90;
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
            if (!InReflectRange(self, player, curStatus)) return;

            // do reflect
            DoReflect(player, self, ref ___velocity, ref ___direction, curStatus);
            player.UseStamina(staminaCost);

            // play melee fx
            var evilReflection = Traverse.Create(player.attackAction);
            var fCD = evilReflection.Field("lastAttackTime");
            var fFlag = evilReflection.Field("running");
            fCD.SetValue(-114514);
            fFlag.SetValue(false);
            if (curStatus != Status.ACTIVE) ExtendStatus(Status.PASSIVE, TIME_PASSIVE_EXTEND);
            player.Attack();
            fCD.SetValue(-114514); // for easily swap to active
            if (curStatus == Status.ACTIVE)
            {
                //fFlag.SetValue(false); no this cancels fx
                ExtendStatus(Status.ACTIVE, TIME_ACTIVE_EXTEND);
            }
        }

        private static void DoReflect(CharacterMainControl player, Projectile self,
            ref Vector3 ___velocity, ref Vector3 ___direction,
            Status curStatus)
        {
            var aimBackProb = ReflectBackChance(curStatus);
            var aimBack = Random.value < aimBackProb;

            if (curStatus == Status.ACTIVE) ModAudio.PlaySoundActive();
            else ModAudio.PlaySoundPassive();

            var oldContext = self.context;
            self.damagedObjects.Clear();
            Vector3 delta;
            if (aimBack) delta = oldContext.fromCharacter.transform.position - self.transform.position;
            else delta = Vector3.Reflect(___velocity, self.transform.position - player.transform.position);
            delta.y = 0;
            oldContext.direction = ___direction = delta.normalized;
            ___velocity = ___direction * oldContext.speed;
            oldContext.team = Teams.player;
            oldContext.fromCharacter = player;

            // extra dmg
            if (curStatus == Status.ACTIVE)
            {
                oldContext.distance *= DISTANCE_MULT_ACTIVE;
                oldContext.critRate = 1;
                oldContext.damage *= DAMAGE_MULT_ACTIVE;
                if (ACTIVE_EXPLOSION && oldContext.explosionRange <= 0)
                {
                    oldContext.explosionRange = 1;
                    oldContext.explosionDamage = oldContext.damage * ACTIVE_EXPLOSION_DAMAGE_FACTOR;
                }
            }
            else
            {
                oldContext.distance *= DISTANCE_MULT_PASSIVE;
                oldContext.damage *= DAMAGE_MULT_PASSIVE;
            }

            self.context = oldContext;
        }
    }
}
