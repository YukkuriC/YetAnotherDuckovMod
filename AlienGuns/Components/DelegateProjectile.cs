using UnityEngine;
using YukkuriC.AlienGuns.Events;

namespace YukkuriC.Misc
{
    public class DelegateProjectile : Projectile
    {
        void Awake()
        {
            direction = Vector3.up;
        }
        bool isPrefab = false;
        public DelegateProjectile SetPrefab()
        {
            isPrefab = true;
            return this;
        }
        void Update()
        {
            if (isPrefab) return;
            AlienGunFireEvents.OnDelegateBulletShoot?.Invoke(this);
            Release();
        }
    }
}
