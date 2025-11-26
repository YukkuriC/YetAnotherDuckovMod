using Duckov.Utilities;
using ItemStatsSystem;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Items.Guns
{
    public static class PlagueShotgun
    {
        static Collider[] cachedColliders = new Collider[10];
        public static void Init(Item item, ItemSetting_Gun gun)
        {
            gun.BindCustomFire(p =>
            {
                var context = p.context;
                context.element_Physics = 0;
                var randIdx = Random.Range(0, BulletLib.Bullets.ElementalBullets.Length);
                BulletLib.ShootOneBullet(
                    BulletLib.Bullets.ElementalBullets[randIdx], context, p.transform.position, context.direction,
                    BulletLib.Bullets.ElementalBulletTypes[randIdx], context.speed, context.distance,
                    0, p.context.firstFrameCheckStartPoint
                );
            });
            gun.BindCustomHurt((victim, dmgInfo) =>
            {
                if (!(dmgInfo.fromCharacter?.CurrentHoldItemAgent is ItemAgent_Gun agent)) return;
                var context = new ProjectileContext
                {
                    damage = dmgInfo.damageValue,
                    fromCharacter = dmgInfo.fromCharacter,
                    critDamageFactor = dmgInfo.critDamageFactor,
                    fromWeaponItemID = dmgInfo.fromWeaponItemID,
                    ignoreHalfObsticle = true,
                    speed = agent.BulletSpeed,
                    distance = agent.BulletDistance,
                    armorBreak = dmgInfo.armorBreak,
                    armorPiercing = dmgInfo.armorPiercing - 0.5f,
                };
                if (context.armorPiercing < 0) return;
                var victimChara = victim.TryGetCharacter();
                if (victimChara == null) return;
                var victimDmgReceiver = victimChara.mainDamageReceiver.gameObject;
                var srcPos = dmgInfo.damagePoint;
                var splitCount = victim.IsDead ? 4 : 2;
                var splitAimCount = victim.IsDead ? 2 : 1;
                var offsetY = dmgInfo.damagePoint.y - victimChara.transform.position.y;

                // search nearby enemy pos
                var targets = new List<Vector3>();
                int total = Physics.OverlapSphereNonAlloc(srcPos, context.distance, cachedColliders, GameplayDataSettings.Layers.damageReceiverLayerMask);
                for (int i = 0; i < total; i++)
                {
                    var collider = cachedColliders[i];
                    var health = collider.GetComponent<DamageReceiver>()?.health;
                    if (health == null || health.IsDead) continue;
                    var chara = health.TryGetCharacter();
                    if (chara == null || chara == victimChara || !Team.IsEnemy(chara.Team, dmgInfo.fromCharacter.Team)) continue;
                    var target = chara.transform.position;
                    target.y += offsetY;
                    targets.Add(target);
                }
                var hasTarget = targets.Count >= 1;
                if (!hasTarget) splitCount = 1;
                //if (!hasTarget) return;

                // spawn bullets
                for (int i = 0; i < splitCount; i++)
                {
                    foreach (var elem in dmgInfo.elementFactors)
                    {
                        if (elem.factor <= 0 || !BulletLib.Bullets.ElementalBulletMap.TryGetValue(elem.elementType, out var bullet)) continue;
                        var targetDir = i < splitAimCount && hasTarget ? targets.GetRandom() - srcPos : Vector3.forward.RotateY(Random.value * 360);
                        var shot = BulletLib.ShootOneBullet(bullet, context, srcPos, targetDir, elem.elementType);
                        shot.damagedObjects.Add(victimDmgReceiver);
                    }
                }
            });
        }
    }
}
