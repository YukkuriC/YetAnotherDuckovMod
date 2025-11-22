using ItemStatsSystem;
using UnityEngine;

namespace YukkuriC.AlienGuns.Items
{
    public class ItemUtils
    {
        public static readonly int ITEM_START_ID = "YukkuriC".GetHashCode();

        public static Item CopyItem(int original, int offset, bool writeBack = true)
        {
            var template = ItemAssetsCollection.InstantiateSync(original);
            template.SetTypeID(ITEM_START_ID + offset);
            Object.DontDestroyOnLoad(template);
            if (writeBack) ItemAssetsCollection.AddDynamicEntry(template);
            return template;
        }
    }
}
