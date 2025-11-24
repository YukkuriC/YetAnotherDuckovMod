using Duckov;
using Duckov.Utilities;
using ItemStatsSystem;
using UnityEngine;
using YukkuriC.Misc;

namespace YukkuriC
{
    public static class BulletLib
    {
        /// <summary>
        /// required context params:
        /// damage, fromCharacter <br/>
        /// secondary params:
        /// critDamageFactor, explosionRange, explosionDamage, fromWeaponItemID, ignoreHalfObsticle
        /// </summary>
        public static Projectile ShootOneBullet(
            Projectile prefab, ProjectileContext projectileContext,
            Vector3 _muzzlePoint, Vector3 _shootDirection, ElementTypes element = ElementTypes.physics,
            float speed = 3, float distance = 20, float scatter = 0,
            Vector3? firstFrameCheckStartPoint = null
        )
        {
            if (scatter > 0)
            {
                scatter = Random.Range(-0.5f, 0.5f) * scatter;
                _shootDirection = Quaternion.Euler(0f, scatter, 0f) * _shootDirection;
            }
            _shootDirection.Normalize();

            var projInst = LevelManager.Instance.BulletPool.GetABullet(prefab ?? GameplayDataSettings.Prefabs.DefaultBullet);
            projInst.transform.position = _muzzlePoint;
            projInst.transform.rotation = Quaternion.LookRotation(_shootDirection, Vector3.up);
            if (projectileContext.fromCharacter) projectileContext.team = projectileContext.fromCharacter.Team;
            projectileContext.firstFrameCheck = true;
            projectileContext.firstFrameCheckStartPoint = firstFrameCheckStartPoint ?? _muzzlePoint;
            projectileContext.direction = _shootDirection;
            projectileContext.speed = speed;
            projectileContext.distance = distance;
            projectileContext.halfDamageDistance = projectileContext.distance * 0.5f;
            if (projectileContext.critDamageFactor <= 0) projectileContext.critDamageFactor = 2;
            switch (element)
            {
                default:
                    break;
                case ElementTypes.physics:
                    projectileContext.element_Physics = 1f;
                    break;
                case ElementTypes.fire:
                    projectileContext.element_Fire = 1f;
                    break;
                case ElementTypes.poison:
                    projectileContext.element_Poison = 1f;
                    break;
                case ElementTypes.electricity:
                    projectileContext.element_Electricity = 1f;
                    break;
                case ElementTypes.space:
                    projectileContext.element_Space = 1f;
                    break;
                case ElementTypes.ghost:
                    projectileContext.element_Ghost = 1f;
                    break;
            }
            projInst.Init(projectileContext);
            return projInst;
        }
        public static class Bullets
        {
            public static readonly Projectile BulletDelegate;
            public static readonly Projectile BulletStorm = GetOriginalBulletPrefab(902);
            public static readonly Projectile BulletRed = GetOriginalBulletPrefab(1239);

            public static Projectile GetOriginalBulletPrefab(int gunId, bool copy = false, bool dontDestroy = true)
            {
                Projectile ret = ItemAssetsCollection.GetPrefab(gunId)
                    ?.GetComponent<ItemSetting_Gun>()
                    ?.bulletPfb
                    ?? GameplayDataSettings.Prefabs.DefaultBullet;
                if (copy) ret = Object.Instantiate(ret);
                if (dontDestroy) Object.DontDestroyOnLoad(ret);
                ret.gameObject.SetActive(false);
                return ret;
            }

            static Bullets()
            {
                BulletDelegate = GetOriginalBulletPrefab(0, true);
                var copyProj = GetOriginalBulletPrefab(0, true);
                var go = copyProj.gameObject;
                Object.Destroy(go.GetComponent<Projectile>());
                BulletDelegate = go.AddComponent<DelegateProjectile>().SetPrefab();
            }
        }
    }
}
