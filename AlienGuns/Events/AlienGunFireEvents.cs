using System;
using System.Collections.Generic;
using System.Text;
using YukkuriC.Events;

namespace YukkuriC.AlienGuns.Events
{
    public static class AlienGunFireEvents
    {
        public static readonly Dictionary<int, Action<Projectile>> EventsById = new Dictionary<int, Action<Projectile>>();

        public static void OnEnable()
        {
            CommonEvents.OnDelegateBulletShoot += HandleShoot;
        }
        public static void OnDisable()
        {
            CommonEvents.OnDelegateBulletShoot -= HandleShoot;
        }

        static void HandleShoot(Projectile handle)
        {
            if (!EventsById.TryGetValue(handle.context.fromWeaponItemID, out var ev)) return;
            ev(handle);
        }
    }
}
