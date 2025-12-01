using UnityEngine;

namespace YukkuriC.AlienGuns.Events
{
    public static class DoubleTapHandler
    {
        const float VALID_INTERVAL = 0.5f;

        static int lastSlot = -1;
        static float lastTime = -1;

        public static void HandleDoubleTap(int slot)
        {
            var now = Time.time;
            if (slot != lastSlot || now > lastTime + VALID_INTERVAL)
            {
                lastSlot = slot;
                lastTime = now;
                return;
            }
            lastTime = now;

            // try get item & use
            var player = CharacterMainControl.Main;
            if (player == null) return;
            var item = player.GetSlot(lastSlot == 1 ? InputManager.PrimaryWeaponSlotHash : InputManager.SecondaryWeaponSlotHash)?.Content;
            if (item != null) player.UseItem(item);
        }
    }
}
