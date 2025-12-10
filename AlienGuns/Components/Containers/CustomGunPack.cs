using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components.Containers
{
    [CreateAssetMenu(fileName = "New Gun", menuName = "Custom Gun Ref")]
    public class CustomGunPack : ScriptableObject
    {
        public ItemAgent_Gun gun;
        public Projectile bullet;
    }
}
