using ItemStatsSystem;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using YukkuriC.AlienGuns.Components.Containers;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Items.Guns
{
    public static class BFG
    {
        public static void Init(Item item, ItemSetting_Gun gun)
        {
            // prefabs
            var gunRef = "BFG.ab".ToAB().LoadAsset<CustomGunPack>("BFG");
            item.AgentUtilities.agents[0].agentPrefab = gunRef.gun;
            gun.bulletPfb = gunRef.bullet;

            // stats
            item.Stats["BulletSpeed"].BaseValue = 3f;
            item.Stats["Damage"].BaseValue = 50;
            item.Stats["ArmorPiercing"].BaseValue = 65535;
        }
    }
}
