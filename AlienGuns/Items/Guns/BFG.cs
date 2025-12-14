using Duckov.Utilities;
using ItemStatsSystem;
using NodeCanvas.Tasks.Actions;
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
        static readonly CustomData[] BULLET_CONSTANTS = new CustomData[]
        {
            new CustomData("Caliber", "BTC") { Display=true },
            new CustomData("buffChanceMultiplier", 1f),
            new CustomData("DurabilityCost", 0.02f) { Display=true },
        };

        public static void Init(Item item, ItemSetting_Gun gun, Item ammoPiece)
        {
            // prefabs
            var gunRef = "BFG.ab".ToAB().LoadAsset<CustomGunPack>("BFG");
            item.AgentUtilities.agents[0].agentPrefab = gunRef.gun;
            gun.bulletPfb = gunRef.bullet;

            // stats
            item.weight = 20;
            item.Constants.SetString(GunRegistry.HASH_CALIBER, "BTC");
            item.Stats["BulletSpeed"].BaseValue = 7.5f;
            item.Stats["Damage"].BaseValue = 30;
            item.Stats["ArmorPiercing"].BaseValue = 65535;
            item.Stats["Penetrate"].BaseValue = 0;
            gun.element = ElementTypes.space;
            gun.shootKey = "stormboss";

            // ammo stats
            ammoPiece.ApplyAmmoStats(0.5f);
            ItemAssetsCollection.GetPrefab(388).ApplyAmmoStats(5f);
        }

        static void ApplyAmmoStats(this Item target, float damageMult)
        {
            if (target.GetComponent<ItemSetting_Bullet>() == null) target.gameObject.AddComponent<ItemSetting_Bullet>();
            target.Constants.Add(new CustomData("damageMultiplier", damageMult) { Display = true });
            foreach (var entry in BULLET_CONSTANTS) target.Constants.Add(entry);
            var tag = ScriptableObject.CreateInstance<Tag>();
            tag.name = "Bullet";
            tag.show = true;
            target.Tags.Add(tag);
        }
    }
}
