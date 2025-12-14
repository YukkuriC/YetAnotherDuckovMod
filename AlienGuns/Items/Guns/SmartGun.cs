using Duckov.Utilities;
using ItemStatsSystem;
using NodeCanvas.Tasks.Actions;
using SodaCraft.Localizations;
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

        static Vector3[] dirSplitTmp = new Vector3[2];
        public static void Init(Item item, ItemSetting_Gun gun, Material redMat)
        {
            item.DisplayQuality = DisplayQuality.Green;
            item.Variables.SetInt("TrackMode", 0);

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
                int splitCount = -1;
                if (nearChara != null)
                    switch (GetTrackMode(agent.Item))
                    {
                        case 1: initDir.y += 0.577f; break;
                        case 2: initDir.y += 1; break;
                        case 3: initDir.y += 1.732f; break;
                        case 4:
                            dirSplitTmp[0] = initDir.RotateY(30);
                            dirSplitTmp[1] = initDir.RotateY(-30);
                            splitCount = 2;
                            p.context.damage /= 2;
                            break;
                    }
                if (splitCount <= 0)
                {
                    splitCount = 1;
                    dirSplitTmp[0] = initDir;
                }
                for (int i = 0; i < splitCount; i++)
                {
                    var bullet = BulletLib.ShootOneBullet(lastAmmos ? bulletMarkLowAmmo : moduloMark ? bulletMarkEveryModulo : originalBullet,
                        p.context, p.transform.position,
                        dirSplitTmp[i], firstFrameCheckStartPoint: p.context.firstFrameCheckStartPoint);

                    if (nearChara != null)
                    {
                        bullet.context.ignoreHalfObsticle = true;
                        bullet.GetComponent<SmartBulletTracker>()?.UpdateTarget(nearChara);
                    }
                }
            });

            // using switch track type
            item.AddUseItem<SmartGunControl>();

            var agent = (ItemAgent_Gun)item.CopyAgent();
            var greenMat = new Material(redMat);
            greenMat.SetColor("_EdgeLightColor", new Color(0.5f, 4.5f, 0.5f));
            ItemUtils.AddMaterialToGun(agent, greenMat, clearOriginal: true);
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
                var oldMode = GetTrackMode(item);
                var newMode = (oldMode + (chara.runInput ? 4 : 1)) % 5;
                SetTrackMode(item, newMode);
                chara.PopText(string.Format(LocalizationManager.GetPlainText("YukkuriC.AlienGun.TrackMode.base"), LocalizationManager.GetPlainText($"YukkuriC.AlienGun.TrackMode.{newMode}")));
            }
        }

        static int trackModeHash = "TrackMode".GetHashCode();
        static int GetTrackMode(Item item) => item.Variables.GetInt(trackModeHash);
        static void SetTrackMode(Item item, int newVal) => item.Variables.SetInt(trackModeHash, newVal);
    }
}
