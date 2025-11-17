using Duckov.Utilities;
using ItemStatsSystem;
using UnityEngine;

namespace CashAsAmmo
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        const int CASH_ID = 451;
        static readonly string[] TAGS_ADD = new string[] {
            "Bullet",
        };
        static readonly CustomData[] BULLET_CONSTANTS = new CustomData[]
        {
            new CustomData("Caliber", "AR") { Display=true },
            new CustomData("damageMultiplier", 1f) { Display=true },
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
        }
        void OnDisable()
        {
            var cash = ItemAssetsCollection.GetPrefab(CASH_ID);
            var go = cash.gameObject;

            // simple recovers
            foreach (var tagKey in TAGS_ADD) cash.Tags.Remove(CreateTagWithKey(tagKey));
            var setting = go.GetComponent<ItemSetting_Bullet>();
            if (setting != null) Destroy(setting);
            cash.Constants.Remove(cash.Constants.GetEntry("Caliber"));
        }
        Tag CreateTagWithKey(string tagKey)
        {
            var tag = ScriptableObject.CreateInstance<Tag>();
            tag.name = tagKey;
            return tag;
        }
    }
}
