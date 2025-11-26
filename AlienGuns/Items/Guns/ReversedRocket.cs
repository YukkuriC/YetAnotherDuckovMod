using ItemStatsSystem;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Items.Guns
{
    public static class ReversedRocket
    {
        public static void Init(Item rpg, ItemSetting_Gun gun, Material redMat)
        {
            rpg.Constants.SetString(GunRegistry.HASH_CALIBER, "PWL");
            gun.autoReload = true;

            // custom fire
            gun.BindCustomFire(p =>
            {
                var player = p.context.fromCharacter;
                if (player == null) return;

                // explode & hurt self
                var center = p.transform.position;
                var dmgInfo = new DamageInfo(player)
                {
                    damageValue = 50,
                    fromCharacter = player,
                    fromWeaponItemID = p.context.fromWeaponItemID,
                };
                LevelManager.Instance.ExplosionManager.CreateExplosion(center, 5, dmgInfo, canHurtSelf: false);
                dmgInfo.damageValue /= 2;
                dmgInfo.toDamageReceiver = player.mainDamageReceiver;
                dmgInfo.damagePoint = player.transform.position + Vector3.up * 0.6f;
                dmgInfo.damageNormal = (dmgInfo.damagePoint - center).normalized;
                player.mainDamageReceiver.Hurt(dmgInfo);

                // push chara
                var mover = player.movementControl.characterMovement;
                if (mover.isOnGround)
                {
                    ref var vel = ref mover.velocity;
                    mover.PauseGroundConstraint();
                    vel += (player.CurrentAimDirection * 6 + Vector3.up * 15);
                }
            });

            // model alter
            var agent = rpg.CopyAgent();
            foreach (var target in new string[] { "Model", "Sockets" })
                agent.transform.Find(target).localEulerAngles = new Vector3(45, 180);
            var renderer = agent.transform.Find("Model").GetChild(0).GetComponent<Renderer>();
            renderer.materials = new Material[] { redMat };
        }
    }
}
