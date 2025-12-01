using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace YukkuriC.AlienGuns.Events
{
    public static class AlienGunEvents
    {
        public static Action<Projectile> OnDelegateBulletShoot;
        public static readonly Dictionary<int, Action<Projectile>> FireEventsById = new Dictionary<int, Action<Projectile>>();
        public static readonly Dictionary<int, Action<Health, DamageInfo>> HurtEventsById = new Dictionary<int, Action<Health, DamageInfo>>();

        public static void OnEnable()
        {
            OnDelegateBulletShoot += HandleShoot;
            Health.OnHurt += HandleHurt;
            LevelManager.OnAfterLevelInitialized += InjectPlayerInput;
        }
        public static void OnDisable()
        {
            OnDelegateBulletShoot -= HandleShoot;
            Health.OnHurt -= HandleHurt;
            RecoverInjectPlayerInput();
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

        // weapon slot input inject
        static void InjectPlayerInput()
        {
            RecoverInjectPlayerInput();
            CharacterInputControl.Instance.Bind(GameManager.MainPlayerInput.actions["ItemShortcut1"], OnPrimaryWeaponSlot);
            CharacterInputControl.Instance.Bind(GameManager.MainPlayerInput.actions["ItemShortcut2"], OnSecondaryWeaponSlot);
        }
        static void RecoverInjectPlayerInput()
        {
            if (CharacterInputControl.Instance == null) return;
            CharacterInputControl.Instance.Unbind(GameManager.MainPlayerInput.actions["ItemShortcut1"], OnPrimaryWeaponSlot);
            CharacterInputControl.Instance.Unbind(GameManager.MainPlayerInput.actions["ItemShortcut2"], OnSecondaryWeaponSlot);
        }

        static void OnPrimaryWeaponSlot(InputAction.CallbackContext ctx) => OnWeaponSlot(1, ctx);
        static void OnSecondaryWeaponSlot(InputAction.CallbackContext ctx) => OnWeaponSlot(2, ctx);
        static void OnWeaponSlot(int slotIdx, InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            DoubleTapHandler.HandleDoubleTap(slotIdx);
        }
    }
}
