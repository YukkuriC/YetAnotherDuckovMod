using Duckov.Utilities;
using ItemStatsSystem;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC.AlienGuns.Components;

namespace YukkuriC.AlienGuns.Items.Guns
{
    public class GaussSniper
    {
        public static void Init(Item item, ItemSetting_Gun gun)
        {
            item.Stats.Add(new Stat("BuffChance", 0.3f, true));
            gun.element = ElementTypes.space;
            gun.buff = GameplayDataSettings.Buffs.Space;

            var laserBullet = BulletLib.BulletSpace.MakeLaserBullet();
            gun.bulletPfb = laserBullet;

            var agent = (ItemAgent_Gun)item.CopyAgent();
            ItemUtils.AddMaterialToGun(agent, "Skin_StormCreature");

            // aim marker
            var markerLaser = Object.Instantiate(laserBullet);
            Object.DontDestroyOnLoad(markerLaser);
            markerLaser.trailMaxAlpha /= 2;
            markerLaser.trailMaxWidth /= 2;
            markerLaser.doDamage = false;
            markerLaser.maxReflectCount = 2;
            markerLaser.recycleTarget /= 2;
            var shooter = agent.gameObject.AddComponent<AutoShoot>();
            shooter.bullet = markerLaser;
            shooter.interval = 0.5f;
            shooter.context.distance = 30;
            shooter.RecordContextFix();
        }
    }
}
