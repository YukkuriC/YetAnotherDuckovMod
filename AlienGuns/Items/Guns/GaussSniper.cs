using Duckov.Utilities;
using ItemStatsSystem;
using System.Collections.Generic;
using UnityEngine;

namespace YukkuriC.AlienGuns.Items.Guns
{
    public class GaussSniper
    {
        public static void Init(Item item, ItemSetting_Gun gun)
        {
            item.DisplayQuality = DisplayQuality.Purple;
            item.Stats.Add(new Stat("BuffChance", 0.3f, true));
            gun.element = ElementTypes.space;
            gun.buff = GameplayDataSettings.Buffs.Space;

            var laserBullet = BulletLib.MakeLaserBullet(BulletLib.BulletSpace);
            gun.bulletPfb = laserBullet;

            var agent = (ItemAgent_Gun)item.CopyAgent();
            var renderer = agent.GetComponent<CharacterSubVisuals>().renderers[0];
            var tmpList = new List<Material>();
            renderer.GetMaterials(tmpList);
            tmpList.Add(ResourceGrabber.Get<Material>("Skin_StormCreature"));
            renderer.SetMaterials(tmpList);
        }
    }
}
