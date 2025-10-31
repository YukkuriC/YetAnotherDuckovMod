using HarmonyLib;
using UnityEngine;

namespace ProjectileReflector
{
    [HarmonyPatch]
    public static class ProjectileReflectorWorker
    {
        #region properties
        const double REFLECT_RANGE_SQR = 2 * 2;

        private static bool ShouldCheckReflect(Projectile self)
        {
            // TODO check input
            return true;
        }
        #endregion

        [HarmonyPrefix, HarmonyPatch(typeof(Projectile), "UpdateMoveAndCheck")]
        public static void UpdateMove(Projectile __instance, ref Vector3 ___velocity, ref Vector3 ___direction)
        {
            var self = __instance;
            if (self.context.team == Teams.player) return;
            var player = LevelManager.Instance.MainCharacter;
            if (player == null || !(player.CurrentHoldItemAgent is ItemAgent_MeleeWeapon)) return;

            // check melee range & angle
            var delta = self.transform.position - player.transform.position;
            delta.y = 0;
            if (
                delta.sqrMagnitude >= REFLECT_RANGE_SQR
                || Vector3.Angle(delta, player.CurrentAimDirection) > 90
                || Vector3.Angle(___velocity, player.CurrentAimDirection) < 90
            ) return;

            // do reflect
            var aimBackProb = 0.3f;
            DoReflect(player, self, ref ___velocity, ref ___direction, Random.value < aimBackProb);

            // play melee fx
            var evilReflection = Traverse.Create(player.attackAction);
            evilReflection.Field("lastAttackTime").SetValue(-114514);
            evilReflection.Field("running").SetValue(false);
            player.Attack();
        }

        private static void DoReflect(CharacterMainControl player, Projectile self,
            ref Vector3 ___velocity, ref Vector3 ___direction,
            bool aimBack = true)
        {
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
            self.context = oldContext;
        }
    }
}
