using ItemStatsSystem;
using ItemStatsSystem.Items;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC.AlienGuns.Components;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Items
{
    public static class GunRegistry
    {
        public static List<Item> AddedGuns = new List<Item>();

        static int HASH_CALIBER = "Caliber".GetHashCode();
        static bool inited = false;
        static int idOffset = 0;
        static Item GetNew(int templateId, out ItemSetting_Gun gun)
        {
            var res = ItemUtils.CopyItem(templateId, idOffset);
            Debug.Log($"[AlienGun] register item #{res.TypeID} from {res.DisplayName}#{templateId}");
            res.DisplayNameRaw = $"YukkuriC.AlienGun.{idOffset}";
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

            // 0. alien hand
            Material redMat;
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
                    var bullet = isRed ? BulletLib.Bullets.BulletRed : BulletLib.Bullets.BulletStorm;
                    gunItem?.Constants.SetBool("red", !isRed);
                    var left = context.direction.RotateY(90);
                    for (var i = -2; i <= 2; i++)
                    {
                        var dir = context.direction.RotateY(i * 10 * (isRed ? 1 : -0.2f));
                        var speed = (context.speed - Mathf.Abs(i) * 2);
                        if (!isRed) speed *= 1.2f;
                        BulletLib.ShootOneBullet(bullet, context, p.transform.position + left * (i * 0.2f), dir, isRed ? ElementTypes.fire : ElementTypes.space, speed, context.distance - Mathf.Abs(i), 20 - Mathf.Abs(i) * 5, context.firstFrameCheckStartPoint);
                    }
                });

                // edit model
                var agentHand = hand.CopyAgent();
                var agentSubVisuals = agentHand.GetComponent<CharacterSubVisuals>();
                var handModel = agentHand.transform.Find("Model/GunModel/WPN_DryHand");
                handModel.localRotation = Quaternion.Euler(85, 0, 0);
                var armWaver = handModel.gameObject.AddComponent<MageHandWaver>();
                for (int i = -1; i <= 1; i += 2)
                {
                    var subHand = Object.Instantiate(handModel);
                    subHand.transform.SetParent(handModel.parent);
                    subHand.localRotation = Quaternion.Euler(69, 85 * i, 100 * i);
                    subHand.localScale = handModel.localScale;
                    subHand.localPosition = new Vector3(-0.15f * i, 0, -0.77f);
                    agentSubVisuals.renderers.Add(subHand.GetComponent<Renderer>());
                    if (i < 0) armWaver.arm1 = subHand;
                    else armWaver.arm2 = subHand;
                }
                var renderer = handModel.GetComponent<Renderer>();
                var mat = renderer.material;
                redMat = new Material(mat);
                mat.SetColor("_EdgeLightColor", new Color(4.5f, 0.5f, 4.5f));
                renderer.material = mat;
            }

            // 1. reversed rocket
            {
                var rpg = GetNew(327, out var gun);
                rpg.Constants.SetString(HASH_CALIBER, "PWL");
                gun.autoReload = true;

                // custom fire
                gun.BindCustomFire(p =>
                {
                    var player = p.context.fromCharacter;
                    if (player == null) return;

                    // explode & hurt self
                    var center = p.transform.position;
                    var dmgInfo = new DamageInfo(player)
                    {
                        damageValue = 50,
                        fromCharacter = player,
                        fromWeaponItemID = p.context.fromWeaponItemID,
                    };
                    LevelManager.Instance.ExplosionManager.CreateExplosion(center, 5, dmgInfo, canHurtSelf: false);
                    dmgInfo.damageValue /= 2;
                    dmgInfo.toDamageReceiver = player.mainDamageReceiver;
                    dmgInfo.damagePoint = player.transform.position + Vector3.up * 0.6f;
                    dmgInfo.damageNormal = (dmgInfo.damagePoint - center).normalized;
                    player.mainDamageReceiver.Hurt(dmgInfo);

                    // push chara
                    var mover = player.movementControl.characterMovement;
                    if (mover.isOnGround)
                    {
                        ref var vel = ref mover.velocity;
                        mover.PauseGroundConstraint();
                        vel += (player.CurrentAimDirection * 6 + Vector3.up * 15);
                    }
                });

                // model alter
                var agent = rpg.CopyAgent();
                foreach (var target in new string[] { "Model", "Sockets" })
                    agent.transform.Find(target).localEulerAngles = new Vector3(45, 180);
                var renderer = agent.transform.Find("Model").GetChild(0).GetComponent<Renderer>();
                renderer.materials = new Material[] { redMat };
            }
        }
    }
}
