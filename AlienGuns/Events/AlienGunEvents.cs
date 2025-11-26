using System;
using System.Collections.Generic;

namespace YukkuriC.AlienGuns.Events
{
    public static class AlienGunEvents
    {
        public static event Action<Projectile> OnDelegateBulletShoot;
        public static readonly Dictionary<int, Action<Projectile>> FireEventsById = new Dictionary<int, Action<Projectile>>();
        public static readonly Dictionary<int, Action<Health, DamageInfo>> HurtEventsById = new Dictionary<int, Action<Health, DamageInfo>>();

        public static void OnEnable()
        {
            OnDelegateBulletShoot += HandleShoot;
            Health.OnHurt += HandleHurt;
        }
        public static void OnDisable()
        {
            OnDelegateBulletShoot -= HandleShoot;
            Health.OnHurt -= HandleHurt;
        }

        static void HandleShoot(Projectile handle)
        {
            if (!FireEventsById.TryGetValue(handle.context.fromWeaponItemID, out var ev)) return;
            ev(handle);
        }
        static void HandleHurt(Health victim, DamageInfo dmg)
        {
            if (!HurtEventsById.TryGetValue(dmg.fromWeaponItemID, out var ev)) return;
            ev(victim, dmg);
        }
    }
}
