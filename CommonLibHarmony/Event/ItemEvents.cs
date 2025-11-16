using System;
using System.Collections.Generic;
using System.Text;

namespace YukkuriC.Event
{
    public static class ItemEvents
    {
        public static Action<GunShotEvent> OnPreShootGun;

        public class GunShotEvent : BaseEvent
        {
            public readonly CharacterMainControl shooter;
            public readonly ItemAgent_Gun gun;
            public GunShotEvent(CharacterMainControl c, ItemAgent_Gun g)
            {
                shooter = c;
                gun = g;
            }
        }
    }
}
