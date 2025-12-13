using ItemStatsSystem;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;
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
            item.weight = 20;
            item.Constants.SetString(GunRegistry.HASH_CALIBER, "PWL");
            item.Stats["BulletSpeed"].BaseValue = 7.5f;
            item.Stats["Damage"].BaseValue = 50;
            item.Stats["ArmorPiercing"].BaseValue = 65535;
            item.Stats["Penetrate"].BaseValue = 0;
            gun.element = ElementTypes.space;
            gun.shootKey = "stormboss";
        }
    }
}
