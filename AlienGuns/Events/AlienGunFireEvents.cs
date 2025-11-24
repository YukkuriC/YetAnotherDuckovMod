using System;
using System.Collections.Generic;

namespace YukkuriC.AlienGuns.Events
{
    public static class AlienGunFireEvents
    {
        public static Action<Projectile> OnDelegateBulletShoot;
        public static readonly Dictionary<int, Action<Projectile>> EventsById = new Dictionary<int, Action<Projectile>>();

        public static void OnEnable()
        {
            OnDelegateBulletShoot += HandleShoot;
        }
        public static void OnDisable()
        {
            OnDelegateBulletShoot -= HandleShoot;
        }

        static void HandleShoot(Projectile handle)
        {
            if (!EventsById.TryGetValue(handle.context.fromWeaponItemID, out var ev)) return;
            ev(handle);
        }
    }
}
