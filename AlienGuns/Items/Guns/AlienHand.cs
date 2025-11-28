using ItemStatsSystem;
using ItemStatsSystem.Items;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC.AlienGuns.Components;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Items.Guns
{
    public static class AlienHand
    {
        public static void Init(Item hand, ItemSetting_Gun gun, out Material redMat)
        {
            var redHash = "red".GetHashCode();

            // values
            hand.Tags.Remove("DestroyOnLootBox");
            hand.Tags.Add("GunType_BR", "Special", "Repairable");
            hand.Slots.Add(new Slot("Tec").With("TecEquip"));
            hand.Constants.SetString(GunRegistry.HASH_CALIBER, "PWL");

            // custom fire
            gun.BindCustomFire(p =>
            {
                var context = p.context;
                context.element_Fire = 0;
                var gunItem = p.GetGun()?.Item;
                var isRed = gunItem?.Constants.GetBool(redHash) ?? false;
                var bullet = isRed ? BulletLib.BulletRed : BulletLib.BulletStorm;
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
    }
}
