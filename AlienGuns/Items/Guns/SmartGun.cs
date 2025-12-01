using Duckov.Utilities;
using ItemStatsSystem;
using NodeCanvas.Tasks.Actions;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC.AlienGuns.Components;
using YukkuriC.AlienGuns.Ext;
using YukkuriC.AlienGuns.Interfaces;

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
                // search near aim target
                var nearChara = PickNearAim(player);
                var initDir = p.context.direction;
                //if (nearChara != null) initDir.y += 1;

                var bullet = BulletLib.ShootOneBullet(lastAmmos ? bulletMarkLowAmmo : moduloMark ? bulletMarkEveryModulo : originalBullet,
                    p.context, p.transform.position,
                    initDir, firstFrameCheckStartPoint: p.context.firstFrameCheckStartPoint);

                if (nearChara != null)
                {
                    bullet.context.ignoreHalfObsticle = true;
                    bullet.GetComponent<SmartBulletTracker>()?.UpdateTarget(nearChara);
                }
            });

            // using switch ammo type
            item.AddUseItem<SmartGunControl>();
        }

        // use item
        public class SmartGunControl : UsageBehavior, ISetMasterItem
        {
            public Item master;
            public override bool CanBeUsed(Item item, object user) => true;
            public void SetMaster(Item master) => this.master = master;

            protected override void OnUse(Item item, object user)
            {
                var chara = user as CharacterMainControl;
                if (chara == null) return;
                // TODO
                chara.PopText(master.ToString());
            }
        }
    }
}
