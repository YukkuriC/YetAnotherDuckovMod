using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Duckov.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace YukkuriC.Skills
{
    public class FlyBlade
    {
        private const int FLY_COUNT = 10;
        private const float FLY_STEP = 2f;
        private const float BLADE_SIZE = 3f;
        private const float BLADE_STRENGTH = 0.5f;
        private const float STEP_INTERVAL = 0.1f;

        static Collider[] hitCache = new Collider[10];
        public static void CreateBlade(CharacterMainControl player, ItemAgent_MeleeWeapon melee, Vector3 at,
            Vector3 dir, float range, float strength, HashSet<DamageReceiver> ignore)
        {
            // both fx
            FxLib.CreateSlash(melee, at, Quaternion.LookRotation(dir, Vector3.up), range);
            FxLib.CreateSlash(melee, at, Quaternion.LookRotation(-dir, Vector3.up), range);

            // damage collider
            var count = Physics.OverlapSphereNonAlloc(at, range + 0.05f, hitCache,
                GameplayDataSettings.Layers.damageReceiverLayerMask);
            for (var i = 0; i < count; i++)
            {
                var hit = hitCache[i];
                var component = hit.GetComponent<DamageReceiver>();
                if (component == null || !Team.IsEnemy(component.Team, player.Team)) continue;
                if (ignore != null && ignore.Contains(component)) continue;
                var health = component.health;
                if (health != null)
                {
                    var unit = health.TryGetCharacter();
                    if (unit != null && unit.Dashing) continue;
                }

                // do damage
                var delta = (hit.transform.position - player.transform.position);
                delta.y = 0;
                delta = delta.normalized;
                var damageInfo = new DamageInfo(player)
                {
                    damageValue = melee.Damage * melee.CharacterDamageMultiplier * strength,
                    armorPiercing = melee.ArmorPiercing,
                    critDamageFactor = melee.CritDamageFactor * (1f + melee.CharacterCritDamageGain),
                    critRate = melee.CritRate * (1f + melee.CharacterCritRateGain),
                    crit = -1,
                    damageNormal = -player.modelRoot.right,
                    damagePoint = hit.transform.position - delta * 0.2f,
                    fromWeaponItemID = melee.Item.TypeID,
                    bleedChance = melee.BleedChance,
                };
                damageInfo.damagePoint.y = melee.transform.position.y;
                component.Hurt(damageInfo);
                component.AddBuff(GameplayDataSettings.Buffs.Pain, player);
                if (melee.hitFx != null)
                    Object.Instantiate(melee.hitFx, damageInfo.damagePoint,
                        Quaternion.LookRotation(damageInfo.damageNormal, Vector3.up));

                // add to ignore
                ignore?.Add(component);
            }
        }

        public static void CreateFlyBlade(CharacterMainControl player)
        {
            var melee = player.GetMeleeWeapon();
            if (melee == null) return;
            throwBlade(player, melee).Forget();
        }
        private static async UniTask throwBlade(CharacterMainControl player, ItemAgent_MeleeWeapon melee)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(STEP_INTERVAL));
            var start = player.transform.position;
            start.y = melee.transform.position.y;
            var dir = player.modelRoot.forward;
            var step = dir * FLY_STEP;
            var ignore = new HashSet<DamageReceiver>();
            for (var i = 0; i < FLY_COUNT; i++)
            {
                if (player == null) return;
                start += step;
                CreateBlade(player, melee, start, dir, BLADE_SIZE, BLADE_STRENGTH, ignore);
                await UniTask.Delay(TimeSpan.FromSeconds(STEP_INTERVAL));
            }
        }
    }
}