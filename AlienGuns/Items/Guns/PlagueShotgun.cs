using Duckov.Buffs;
using Duckov.Utilities;
using ItemStatsSystem;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Items.Guns
{
    public static class PlagueShotgun
    {
        static Buff FromElement(ElementTypes elem)
        {
            switch (elem)
            {
                case ElementTypes.fire:
                    return GameplayDataSettings.Buffs.Burn;
                case ElementTypes.poison:
                    return GameplayDataSettings.Buffs.Poison;
                case ElementTypes.electricity:
                    return GameplayDataSettings.Buffs.Electric;
                case ElementTypes.space:
                    return GameplayDataSettings.Buffs.Space;
            }
            return null;
        }

        static Collider[] cachedColliders = new Collider[10];
        public static void Init(Item item, ItemSetting_Gun gun,Transform stormFist)
        {
            item.Stats.Add(new Stat("BuffChance", 0.2f, true));
            gun.BindCustomFire(p =>
            {
                var context = p.context;
                context.element_Physics = 0;
                var randIdx = Random.Range(0, BulletLib.Bullets.ElementalBullets.Length);
                var elem = BulletLib.Bullets.ElementalBulletTypes[randIdx];
                context.buff = FromElement(elem);
                BulletLib.ShootOneBullet(
                    BulletLib.Bullets.ElementalBullets[randIdx], context, p.transform.position, context.direction,
                    elem, context.speed, context.distance,
                    0, p.context.firstFrameCheckStartPoint
                );
            });
            gun.BindCustomHurt((victim, dmgInfo) =>
            {
                if (dmgInfo.isFromBuffOrEffect) return;
                if (!(dmgInfo.fromCharacter?.CurrentHoldItemAgent is ItemAgent_Gun agent)) return;
                var newPiercing = dmgInfo.armorPiercing - 0.5f;
                if (newPiercing < 0) return;
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
                    armorPiercing = newPiercing,
                    buff = dmgInfo.buff,
                    buffChance = dmgInfo.buffChance,
                };
                var victimChara = victim.TryGetCharacter();
                if (victimChara == null) return;
                var victimDmgReceiver = victimChara.mainDamageReceiver.gameObject;
                var srcPos = dmgInfo.damagePoint;
                var splitCount = victim.IsDead ? 6 : 2;
                var splitAimCount = victim.IsDead ? 4 : 1;
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

            var agent = item.CopyAgent();
            var modelRoot = agent.transform.Find("Model");
            stormFist = Object.Instantiate(stormFist);
            stormFist.SetParent(modelRoot);
            stormFist.localPosition = new Vector3(0, 0.07f, 0.74f);
            stormFist.localEulerAngles = new Vector3(0, 90, 0);
            stormFist.localScale = Vector3.one * 0.5f;
            agent.GetComponent<CharacterSubVisuals>().SetRenderers();
        }
    }
}
