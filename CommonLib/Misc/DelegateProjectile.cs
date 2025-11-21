using UnityEngine;
using YukkuriC.Events;

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
            CommonEvents.OnDelegateBulletShoot?.Invoke(this);
            Release();
        }
    }
}
