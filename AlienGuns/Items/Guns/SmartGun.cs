using ItemStatsSystem;
using System;
using System.Collections.Generic;
using System.Text;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Items.Guns
{
    public static class SmartGun
    {
        public static void Init(Item item, ItemSetting_Gun gun)
        {
            var originalBullet = gun.bulletPfb;
            var bulletMarkLowAmmo = BulletLib.BulletRed;
            var bulletMarkEveryModulo = BulletLib.BulletPoison;
            gun.BindCustomFire(p =>
            {
                var agent = p.context.fromCharacter?.CurrentHoldItemAgent;
                if (!(agent is ItemAgent_Gun myGun)) return;
                var moduloMark = myGun.BulletCount % 5 == 0;
                var lastAmmos = myGun.BulletCount < 5;
                BulletLib.ShootOneBullet(lastAmmos ? bulletMarkLowAmmo : moduloMark ? bulletMarkEveryModulo : originalBullet,
                    p.context, p.transform.position,
                    p.context.direction, firstFrameCheckStartPoint: p.context.firstFrameCheckStartPoint);
            });
        }
    }
}
