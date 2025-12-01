using Duckov.Utilities;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components
{
    public class SmartBulletTracker : MonoBehaviour
    {
        public Projectile bullet;
        public float updateDistStep = 3;
        public float enemyHeightOffset = 1f;
        public float checkRange = 8;
        public float trackingSpeedScale = 0.5f;

        CharacterMainControl target;
        float updatedDist;

        void OnEnable()
        {
            updatedDist = 0;
            target = null;
        }
        public void UpdateTarget(CharacterMainControl tar)
        {
            target = tar;
        }

        void Update()
        {
            if (target == null || target.Dashing) return;
            if (updatedDist + updateDistStep < bullet.traveledDistance)
            {
                // do track
                var src = bullet.transform.position;
                var dst = target.transform.position;
                dst.y += enemyHeightOffset;
                var dir = (dst - src).normalized;
                var checkReach = Physics.Raycast(src, dir, out var hit, checkRange, bullet.hitLayers);
                if (
                    checkReach
                    && hit.collider.GetComponent<DamageReceiver>()?.health?.TryGetCharacter() == target
                )
                {
                    bullet.direction = bullet.context.direction = dir;
                    var newVel = dir * (bullet.context.speed * trackingSpeedScale);
                    bullet.velocity = (bullet.velocity + newVel) / 2;
                }
                else
                {
                    updatedDist += updateDistStep;
                }
            }
        }
    }
}
