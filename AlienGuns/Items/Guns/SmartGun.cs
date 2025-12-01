using Duckov.Utilities;
using ItemStatsSystem;
using UnityEngine;
using YukkuriC.AlienGuns.Components;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Items.Guns
{
    public static class SmartGun
    {
        static Collider[] cachedResults;
        static CharacterMainControl PickNearAim(CharacterMainControl player, float range = 10)
        {
            if (player == null || !player.IsInAdsInput) return null;
            if (cachedResults == null) cachedResults = new Collider[10];
            var src = player.GetCurrentAimPoint();
            var count = Physics.OverlapSphereNonAlloc(src, range, cachedResults, GameplayDataSettings.Layers.damageReceiverLayerMask);
            CharacterMainControl res = null;
            var minDist = range * 2;
            for (int i = 0; i < count; i++)
            {
                var collider = cachedResults[i];
                var chara = collider.GetComponent<DamageReceiver>().health?.TryGetCharacter();
                if (chara == null || !Team.IsEnemy(chara.Team, player.Team)) continue;
                var myDist = Vector3.Distance(collider.transform.position, src);
                if (minDist > myDist)
                {
                    minDist = myDist;
                    res = chara;
                }
            }
            return res;
        }

        public static void Init(Item item, ItemSetting_Gun gun)
        {
            item.DisplayQuality = DisplayQuality.Green;

            var originalBullet = gun.bulletPfb.MakeSmartBullet();
            var bulletMarkLowAmmo = BulletLib.BulletRed.MakeSmartBullet();
            var bulletMarkEveryModulo = BulletLib.BulletPoison.MakeSmartBullet();
            bulletMarkEveryModulo.hitFx = bulletMarkLowAmmo.hitFx = originalBullet.hitFx;
            gun.BindCustomFire(p =>
            {
                var player = p.context.fromCharacter;
                var agent = player?.CurrentHoldItemAgent;
                if (!(agent is ItemAgent_Gun myGun)) return;
                var moduloMark = myGun.BulletCount % 5 == 0;
                var lastAmmos = myGun.BulletCount < 5;
                var bullet = BulletLib.ShootOneBullet(lastAmmos ? bulletMarkLowAmmo : moduloMark ? bulletMarkEveryModulo : originalBullet,
                    p.context, p.transform.position,
                    p.context.direction, firstFrameCheckStartPoint: p.context.firstFrameCheckStartPoint);

                // search near aim target
                var nearChara = PickNearAim(player);
                if (nearChara != null)
                {
                    bullet.context.ignoreHalfObsticle = true;
                    bullet.GetComponent<SmartBulletTracker>()?.UpdateTarget(nearChara);
                }
            });
        }
    }
}
