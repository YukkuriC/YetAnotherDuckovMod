using ItemStatsSystem;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC.AlienGuns.Ext;
using YukkuriC.AlienGuns.Items.Guns;

namespace YukkuriC.AlienGuns.Items
{
    public static class GunRegistry
    {
        public static List<Item> AddedGuns = new List<Item>();

        public static int HASH_CALIBER = "Caliber".GetHashCode();
        static bool inited = false;
        static int idOffset = 0;
        static Item GetNew(int templateId, out ItemSetting_Gun gun)
        {
            var res = ItemUtils.CopyItem(templateId, idOffset);
            Debug.Log($"[AlienGun] register item #{res.TypeID} from {res.DisplayName}#{templateId}");
            res.DisplayNameRaw = $"YukkuriC.AlienGun.{idOffset}";
            res.Quality = 8;
            AddedGuns.Add(res);
            var icon = $"assets/textures/icon_{idOffset}.png".ToLooseTexture();
            if (icon != null) res.Icon = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), Vector2.zero);
            idOffset++;
            gun = res.GetComponent<ItemSetting_Gun>();
            return res;
        }

        public static void Init()
        {
            if (inited) return;
            inited = true;

            // prepare parts
            Transform partStormFist;
            {
                partStormFist = ItemAssetsCollection.GetPrefab(915).GetAgent().transform.Find("Model/GunModel/Pfb_WPN_SonicFist_P/pCylinder6");
            }

            // 0. alien hand
            Material redMat;
            {
                var hand = GetNew(1239, out var gun);
                AlienHand.Init(hand, gun, out redMat);
            }

            // 1. reversed rocket
            {
                var rpg = GetNew(327, out var gun);
                ReversedRocket.Init(rpg, gun, redMat);
            }

            // 2. elemental shotgun
            {
                var item = GetNew(876, out var gun);
                PlagueShotgun.Init(item, gun, partStormFist);
            }

            // 3. laser gun
            {
                var item = GetNew(407, out var gun);
                GaussSniper.Init(item, gun);
            }

            // 4. smart gun
            {
                var item = GetNew(788, out var gun);
                SmartGun.Init(item, gun);
            }

            // 5. chick spawner
            {
                var item = GetNew(1260, out var gun);
                ChickSpawner.Init(item, gun);
            }

            // 6. BFG
            {
                var item = GetNew(407, out var gun);
                BFG.Init(item, gun);
            }
        }
    }
}
