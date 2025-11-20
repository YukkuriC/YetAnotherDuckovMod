using HarmonyLib;
using UnityEngine;

namespace ProjectileReflector
{
    using static ReflectController;
    using static ModConfigs;

    [HarmonyPatch]
    public static class GrenadeReflectorWorker
    {
        [HarmonyPrefix, HarmonyPatch(typeof(Grenade), "Update")]
        static void DoGrenadeReflect(Grenade __instance, Rigidbody ___rb)
        {
            if (!LevelManager.LevelInited) return;
            if (!ReflectController.DoReflectGrenade) return;
            var self = __instance;
            var player = LevelManager.Instance.MainCharacter;
            if (
                player == null
                || !(player.CurrentHoldItemAgent is ItemAgent_MeleeWeapon melee)
                || !InReflectRange(self.transform, player, Status.ACTIVE, out var _)
            ) return;

            // decide horizontal dir
            Vector3 velocity;
            ref var dmgInfo = ref self.damageInfo;
            var thrower = dmgInfo.fromCharacter;
            if (AIM_GRENADE_OWNER && thrower != null && thrower != player) velocity = thrower.transform.position - self.transform.position;
            else velocity = self.transform.position - player.transform.position;
            velocity.y = 0;
            velocity.Normalize();

            // do throw
            velocity *= GRENADE_REFLECT_HORIZONTAL_SPEED;
            velocity.y = GRENADE_REFLECT_VERTICAL_SPEED;
            ___rb.velocity = velocity;
            dmgInfo.fromCharacter = player;
        }
    }
}
