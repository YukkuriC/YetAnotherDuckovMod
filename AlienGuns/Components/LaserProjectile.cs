using Duckov.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Components
{
    public class LaserProjectile : Projectile
    {
        // consts
        const float SAME_HIT_RANGE = 0.5f;

        // configs
        public float trailMaxWidth = 0.5f;
        public float trailMaxAlpha = 1;
        public float recycleTarget = 0.5f;
        public bool doDamage = true;
        public int maxReflectCount = 999;

        // tmp vars
        bool usedUp;
        float recycleTimer;
        Collider lastHitCollider;
        int reflectCount;

        public void Awake()
        {
            if (trail != null)
            {
                trail.time = recycleTarget;
            }
        }

        public void OnEnable()
        {
            usedUp = false;
            recycleTimer = 0;
            lastHitCollider = null;
            reflectCount = maxReflectCount;
        }
        public void Update()
        {
            if (usedUp)
            {
                recycleTimer += Time.deltaTime;
                if (recycleTimer > recycleTarget)
                {
                    Release();
                }
                else
                {
                    var fade = 1 - recycleTimer / recycleTarget;
                    UpdateFadeOut(fade);
                }
                return;
            }

            var start = context.firstFrameCheckStartPoint;
            var hits = Physics.SphereCastAll(start, radius, direction, context.distance, hitLayers, QueryTriggerInteraction.Ignore).ToList();
            hits.Sort((x, y) => (int)Mathf.Sign(x.distance - y.distance));

            var penetrateAll = true;
            foreach (var hit in hits)
            {
                var collider = hit.collider;
                if (collider != lastHitCollider && hit.distance < SAME_HIT_RANGE) continue;
                var receiver = collider.GetComponent<DamageReceiver>();

                // hit obstacles -> reflect & end
                if (receiver == null)
                {
                    context.distance -= hit.distance;
                    MoveSelf(hit.point, Vector3.Reflect(direction, hit.normal));
                    if (hitFx) Instantiate(hitFx, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
                    reflectCount--;
                    if (reflectCount < 0) usedUp = true;
                    return;
                }

                // damage objects
                if (!doDamage) continue;
                if (!Team.IsEnemy(receiver.Team, context.team)) continue;
                var chara = receiver.health?.TryGetCharacter();
                if (chara != null && chara.Dashing) continue;
                var dmgInfo = context.ToDamage();
                if (context.distance - hit.distance < context.halfDamageDistance) dmgInfo.damageValue *= 0.5f;
                dmgInfo.damagePoint = hit.point;
                dmgInfo.damageNormal = hit.normal;
                receiver.Hurt(dmgInfo);
                receiver.AddBuff(GameplayDataSettings.Buffs.Pain, context.fromCharacter);
                context.penetrate--;
                if (context.penetrate < 0)
                {
                    penetrateAll = false;
                    MoveSelf(hit.point);
                    break;
                }
            }

            // move to final
            if (penetrateAll)
            {
                MoveSelf(start + direction * context.distance);
            }

            // use up
            usedUp = true;
        }

        void UpdateFadeOut(float fade)
        {
            if (trail == null) return;
            trail.widthMultiplier = trailMaxWidth * fade;
            var color = trail.startColor;
            color.a = fade * trailMaxAlpha;
            trail.startColor = color;
            color = trail.endColor;
            color.a = fade * trailMaxAlpha;
            trail.endColor = color;
        }
        void MoveSelf(Vector3 pos, Vector3? dir = null)
        {
            transform.position = context.firstFrameCheckStartPoint = pos;
            if (dir != null)
            {
                context.direction = direction = (Vector3)dir;
                UpdateAimDirection();
            }
        }
    }
}
