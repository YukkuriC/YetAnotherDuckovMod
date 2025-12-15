using Duckov.Utilities;
using ItemStatsSystem;
using UnityEngine;
using YukkuriC.AlienGuns.Components.BFG;
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
            //gun.element = ElementBFG; // throw new ArgumentOutOfRangeException()
            gun.shootKey = "stormboss";
            gun.muzzleFxPfb = null;

            // ammo stats
            ammoPiece.MaxStackCount = 100;
            var theRealAmmoTag = ItemAssetsCollection.GetPrefab(594).Tags.list.Find(x => x.name == "Bullet"); // CraftView.CheckFilter用的array查找，憨吧
            ammoPiece.ApplyAmmoStats(1f, theRealAmmoTag);
            ItemAssetsCollection.GetPrefab(388).ApplyAmmoStats(10f, theRealAmmoTag);

            // BFG damage visuals
            GameplayDataSettings.UIStyle.elementDamagePopTextLook.Add(new GameplayDataSettings.UIStyleData.DisplayElementDamagePopTextLook
            {
                elementType = BFGCore.ElementBFG,
                normalSize = 1f,
                critSize = 1.6f,
                color = new Color(0.3f, 1, 0.3f)
            });
        }

        static void ApplyAmmoStats(this Item target, float damageMult, Tag tagAmmo)
        {
            if (target.GetComponent<ItemSetting_Bullet>() == null) target.gameObject.AddComponent<ItemSetting_Bullet>();
            target.Constants.Add(new CustomData("damageMultiplier", damageMult) { Display = true });
            foreach (var entry in BULLET_CONSTANTS) target.Constants.Add(entry);
            target.Tags.Add(tagAmmo);
        }
    }
}
