using CashAsAmmo.Components;
using Duckov.Utilities;
using ItemStatsSystem;
using SodaCraft.Localizations;
using System.Collections.Generic;
using UnityEngine;

namespace CashAsAmmo
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        const int CASH_ID = 451;
        public const string LANG_KEY_POPUP = "YukkuriC.CashAsAmmo.Pop";
        public static string[] ALL_BULLET_TYPES = new string[] {
            "AR", "BR", "SHT", "SNP", "MAG", "PWL", "PWS", "SMG"
        };
        public static int HASH_CALIBER = "Caliber".GetHashCode();

        static readonly string[] TAGS_ADD = new string[] {
            "Bullet",
        };
        static readonly CustomData[] BULLET_CONSTANTS = new CustomData[]
        {
            new CustomData("Caliber", ALL_BULLET_TYPES[0]) { Display=true },
            new CustomData("damageMultiplier", 1f) { Display=true },
            new CustomData("buffChanceMultiplier", 1f) { Display=true },
        };

        void OnEnable()
        {
            var cash = ItemAssetsCollection.GetPrefab(CASH_ID);
            var go = cash.gameObject;

            // tags
            foreach (var tagKey in TAGS_ADD)
            {
                var tag = CreateTagWithKey(tagKey);
                tag.show = true;
                cash.Tags.Add(tag);
            }

            // settings
            if (go.GetComponent<ItemSetting_Bullet>() == null) go.AddComponent<ItemSetting_Bullet>();

            // constants
            foreach (var entry in BULLET_CONSTANTS) cash.Constants.Add(entry);

            // using switch ammo type
            var useItem = cash.usageUtilities = go.AddComponent<UsageUtilities>();
            useItem.behaviors = new List<UsageBehavior>()
            {
                go.AddComponent<SwapAmmoTypeAction>()
            };

            // lang
            LocalizationManager.OnSetLanguage += UpdateLang;
            UpdateLang(LocalizationManager.CurrentLanguage);

            // debug code
            if (!SteamManager.Initialized)
            {
                LevelManager.enemySpawnCountFactor = 100;
            }
        }
        void OnDisable()
        {
            var cash = ItemAssetsCollection.GetPrefab(CASH_ID);
            var go = cash.gameObject;

            // simple recovers
            foreach (var tagKey in TAGS_ADD) cash.Tags.Remove(CreateTagWithKey(tagKey));
            var setting = go.GetComponent<ItemSetting_Bullet>();
            if (setting != null) Destroy(setting);
            cash.Constants.Remove(cash.Constants.GetEntry(HASH_CALIBER));
            Destroy(go.GetComponent<UsageUtilities>());
            Destroy(go.GetComponent<SwapAmmoTypeAction>());
        }
        Tag CreateTagWithKey(string tagKey)
        {
            var tag = ScriptableObject.CreateInstance<Tag>();
            tag.name = tagKey;
            return tag;
        }

        void UpdateLang(SystemLanguage newLang)
        {
            switch (newLang)
            {
                default:
                case SystemLanguage.English:
                    LocalizationManager.SetOverrideText(LANG_KEY_POPUP, "Switch bullet type to");
                    break;
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                    LocalizationManager.SetOverrideText(LANG_KEY_POPUP, "子弹类型切换至");
                    break;
            }
        }
    }
}
