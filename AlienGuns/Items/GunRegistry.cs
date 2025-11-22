using ItemStatsSystem;
using ItemStatsSystem.Items;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC.Ext;

namespace YukkuriC.AlienGuns.Items
{
    public static class GunRegistry
    {
        static List<Item> AddedGuns = new List<Item>();

        static int HASH_CALIBER = "Caliber".GetHashCode();
        static bool inited = false;
        static int idOffset = 0;
        static Item GetNew(int templateId, out ItemSetting_Gun gun)
        {
            var res = ItemUtils.CopyItem(templateId, idOffset);
            res.DisplayNameRaw = $"YukkuriC.AlienGun.{idOffset}";
            Debug.Log($"[AlienGun] register item #{res.TypeID} from {res.DisplayName}#{templateId}");
            AddedGuns.Add(res);
            var icon = $"textures.icon_{idOffset}.png".ToResourceTexture();
            if (icon != null) res.Icon = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), Vector2.zero);
            idOffset++;
            gun = res.GetComponent<ItemSetting_Gun>();
            return res;
        }

        public static void Init()
        {
            if (inited) return;
            inited = true;

            // 0. alien hand
            {
                var redHash = "red".GetHashCode();
                var hand = GetNew(1239, out var gun);

                // values
                hand.Tags.Remove("DestroyOnLootBox");
                hand.Tags.Add("GunType_BR", "Special", "Repairable");
                hand.Slots.Add(new Slot("Tec").With("TecEquip"));
                hand.Constants.SetString(HASH_CALIBER, "PWL");

                // custom fire
                gun.BindCustomFire(p =>
                {
                    var context = p.context;
                    context.element_Fire = 0;
                    var gunItem = p.GetGun()?.Item;
                    var isRed = gunItem?.Constants.GetBool(redHash) ?? false;
                    var bullet = isRed ? FxLib.Bullets.BulletRed : FxLib.Bullets.BulletStorm;
                    gunItem?.Constants.SetBool("red", !isRed);
                    var left = context.direction.RotateY(90);
                    for (var i = -2; i <= 2; i++)
                    {
                        var dir = context.direction.RotateY(i * 10 * (isRed ? 1 : -0.2f));
                        var speed = (context.speed - Mathf.Abs(i) * 2);
                        if (!isRed) speed *= 1.2f;
                        FxLib.ShootOneBullet(bullet, context, p.transform.position + left * (i * 0.2f), dir, isRed ? ElementTypes.fire : ElementTypes.space, speed, context.distance - Mathf.Abs(i), 20 - Mathf.Abs(i) * 5, context.firstFrameCheckStartPoint);
                    }
                });

                // edit model
                var originalAgentHolder = hand.AgentUtilities.agents[0];
                var agentHand = originalAgentHolder.agentPrefab = Object.Instantiate(originalAgentHolder.agentPrefab);
                var agentSubVisuals = agentHand.GetComponent<CharacterSubVisuals>();
                agentHand.gameObject.SetActive(false);
                agentHand.transform.SetParent(hand.transform);
                var handModel = agentHand.transform.Find("Model/GunModel/WPN_DryHand");
                handModel.localRotation = Quaternion.Euler(85, 0, 0);
                for (int i = -1; i <= 1; i += 2)
                {
                    var subHand = Object.Instantiate(handModel);
                    subHand.transform.SetParent(handModel.parent);
                    subHand.localRotation = Quaternion.Euler(69, 79 * i, 98 * i);
                    subHand.localScale = handModel.localScale;
                    subHand.localPosition = handModel.localPosition;
                    agentSubVisuals.renderers.Add(subHand.GetComponent<Renderer>());
                }
                var renderer = handModel.GetComponent<Renderer>();
                var mat = renderer.material;
                mat.SetColor("_EdgeLightColor", new Color(4.5f, 0.5f, 4.5f));
                renderer.material = mat;
            }
        }

#if DEBUG_MENU
        static Rect windowRect = new Rect(200, 200, 300, 500);
        public static void OnDebugGUI()
        {
            var player = LevelManager.Instance?.MainCharacter;
            GUILayout.BeginArea(windowRect);
            foreach (var prefab in AddedGuns)
            {
                if (GUILayout.Button($"#{prefab.TypeID}: {prefab.DisplayName}"))
                {
                    ItemUtilities.SendToPlayer(ItemAssetsCollection.InstantiateSync(prefab.TypeID));
                }
            }
            GUILayout.EndArea();
        }
#endif
    }
}
