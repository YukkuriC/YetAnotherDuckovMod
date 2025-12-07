
using Cysharp.Threading.Tasks;
using Duckov.Economy;
using Duckov.Scenes;
using Duckov.Utilities;
using ItemStatsSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YukkuriC.AlienGuns.Items;

namespace YukkuriC.AlienGuns.Misc
{
    public static class CustomMerchant
    {
        const string SHOP_ID = "Merchant.YukkuriC.AlienGuns";
        static readonly Dictionary<string, Vector3> SHOP_POS_BY_SCENE = new Dictionary<string, Vector3>()
        {
            ["Prologue_Main"] = new Vector3(-49, 9, -13),
            //["Base"] = new Vector3(-1, 0.5f, -84),
            ["Level_GroundZero_Main"] = new Vector3(219.09f, 21.5f, 305.79f),
            ["Level_HiddenWarehouse_Main"] = new Vector3(367.51f, 5.5f, 175.09f),
            ["Level_Farm_Main"] = new Vector3(509.05f, 0.5f, 847.56f),
        };

        static CharacterRandomPreset presetMerchant, presetRed;
        static StockShop shop;
        static CustomMerchant()
        {
            presetMerchant = ResourceGrabber.Get<CharacterRandomPreset>("EnemyPreset_Merchant_Myst");
            presetRed = ResourceGrabber.Get<CharacterRandomPreset>("EnemyPreset_Boss_Red");
            presetMerchant = Object.Instantiate(presetMerchant);
            Object.DontDestroyOnLoad(presetMerchant);

            // set gun
            var weapon = presetMerchant.itemsToGenerate[0];
            var entry = weapon.itemPool.entries[0];
            entry.value.itemTypeID = ItemUtils.ITEM_START_ID;
            weapon.itemPool.entries[0] = entry;
            presetMerchant.itemsToGenerate[0] = weapon;
            var bulletSetting = presetMerchant.bulletQualityDistribution.entries[0];
            bulletSetting.value = 9;
            presetMerchant.bulletQualityDistribution.entries[0] = bulletSetting;

            // copy model
            presetMerchant.facePreset = presetRed.facePreset;
            presetMerchant.usePlayerPreset = true;
            presetMerchant.characterModel = presetRed.characterModel;

            // copy shop
            var shopBase = presetMerchant.specialAttachmentBases[0] = Object.Instantiate(presetMerchant.specialAttachmentBases[0]);
            Object.DontDestroyOnLoad(shopBase);
            shop = shopBase.GetComponent<StockShop>();
            shop.merchantID = SHOP_ID;
            shop.refreshAfterTimeSpan = 50000000;
        }
        static void SpawnMerchantInScene()
        {
            Debug.Log($"AlienGuns sceneId={JsonUtility.ToJson(MultiSceneCore.Instance.SceneInfo)}");
            if (SHOP_POS_BY_SCENE.TryGetValue(MultiSceneCore.Instance.SceneInfo.ID, out var pos))
            {
                SpawnMerchant(pos).Forget();
            }
        }
        static async UniTask SpawnMerchant(Vector3 pos)
        {
            var merchant = await presetMerchant.CreateCharacterAsync(pos, Random.insideUnitSphere, MultiSceneCore.MainScene.Value.buildIndex, null, false);
            merchant.movementControl.ForceSetPosition(pos);
        }
        static void InitShop()
        {
            if (GameplayDataSettings.StockshopDatabase.GetMerchantProfile(SHOP_ID) == null)
            {
                var myProfile = new StockShopDatabase.MerchantProfile
                {
                    merchantID = SHOP_ID,
                    entries = new List<StockShopDatabase.ItemEntry>()
                {
                    new StockShopDatabase.ItemEntry
                    {
                        typeID=1242,
                        maxStock=10,
                        forceUnlock=true,
                        priceFactor=1,
                        possibility=1,
                    },
                    new StockShopDatabase.ItemEntry
                    {
                        typeID=833,
                        maxStock=5,
                        forceUnlock=true,
                        priceFactor=1,
                        possibility=1,
                    },
                    new StockShopDatabase.ItemEntry
                    {
                        typeID=834,
                        maxStock=5,
                        forceUnlock=true,
                        priceFactor=1,
                        possibility=1,
                    },
                }
                };
                foreach (var gun in GunRegistry.AddedGuns)
                {
                    myProfile.entries.Add(new StockShopDatabase.ItemEntry
                    {
                        typeID = gun.TypeID,
                        maxStock = 3,
                        forceUnlock = true,
                        priceFactor = 1,
                        possibility = 1,
                    });
                }
                GameplayDataSettings.StockshopDatabase.merchantProfiles.Add(myProfile);
            }
        }

        public static void OnEnable()
        {
            InitShop();
            LevelManager.OnAfterLevelInitialized += SpawnMerchantInScene;
        }
        public static void OnDisable()
        {
            LevelManager.OnAfterLevelInitialized -= SpawnMerchantInScene;
        }
    }
}
