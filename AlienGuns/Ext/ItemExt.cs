using Duckov.ItemBuilders;
using Duckov.Utilities;
using ItemStatsSystem;
using ItemStatsSystem.Items;
using System;
using UnityEngine;
using YukkuriC.AlienGuns.Events;

namespace YukkuriC.AlienGuns.Ext
{
    public static class ItemExt
    {
        public static void BindCustomFire(this ItemSetting_Gun gun, Action<Projectile> sub)
        {
            gun.bulletPfb = BulletLib.BulletDelegate;
            AlienGunEvents.FireEventsById[gun.Item.TypeID] = sub;
        }
        public static void BindCustomHurt(this ItemSetting_Gun gun, Action<Health, DamageInfo> sub)
        {
            AlienGunEvents.HurtEventsById[gun.Item.TypeID] = sub;
        }
        public static ItemSetting_Gun GetGun(this Projectile proj)
        {
            var agent = proj.context.fromCharacter?.CurrentHoldItemAgent;
            return agent is ItemAgent_Gun gun ? gun.GunItemSetting : null;
        }
        public static ItemBuilder ApplyOverride(this ItemBuilder builder, Item item)
        {
            item.CreateSlotsComponent();
            item.CreateStatsComponent();
            item.CreateInventoryComponent();
            item.CreateModifiersComponent();
            foreach (var desc in builder.slots.Values)
            {
                var slot = new Slot(desc.key);
                slot.requireTags.AddRange(desc.requireTags);
                slot.excludeTags.AddRange(desc.excludeTags);
                item.Slots.Add(slot);
            }
            foreach (var desc in builder.stats.Values)
                item.Stats.Add(new Stat(desc.key, desc.value, desc.display));
            foreach (var constant in builder.constants)
                item.Constants.Add(new CustomData(constant));
            foreach (var variable in builder.variables)
                item.Variables.Add(new CustomData(variable));
            foreach (var modifier in builder.modifiers)
                item.Modifiers.Add(modifier);
            item.Modifiers.ReapplyModifiers();
            return builder;
        }
        public static Item ApplyOverride(this Item item, ItemBuilder builder)
        {
            builder.ApplyOverride(item);
            return item;
        }

        /// <summary>
        /// var not handled: halfDamageDistance, damagePoint, damageNormal
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static DamageInfo ToDamage(ref this ProjectileContext context)
        {
            DamageInfo damageInfo = new DamageInfo(context.fromCharacter);
            damageInfo.damageValue = context.damage;
            damageInfo.critDamageFactor = context.critDamageFactor;
            damageInfo.critRate = context.critRate;
            damageInfo.armorPiercing = context.armorPiercing;
            damageInfo.armorBreak = context.armorBreak;
            damageInfo.elementFactors.Add(new ElementFactor(ElementTypes.physics, context.element_Physics));
            damageInfo.elementFactors.Add(new ElementFactor(ElementTypes.fire, context.element_Fire));
            damageInfo.elementFactors.Add(new ElementFactor(ElementTypes.poison, context.element_Poison));
            damageInfo.elementFactors.Add(new ElementFactor(ElementTypes.electricity, context.element_Electricity));
            damageInfo.elementFactors.Add(new ElementFactor(ElementTypes.space, context.element_Space));
            damageInfo.elementFactors.Add(new ElementFactor(ElementTypes.ghost, context.element_Ghost));
            damageInfo.buffChance = context.buffChance;
            damageInfo.buff = context.buff;
            damageInfo.bleedChance = context.bleedChance;
            damageInfo.damageType = DamageTypes.normal;
            damageInfo.fromWeaponItemID = context.fromWeaponItemID;
            return damageInfo;
        }
        public static bool Attack(ref this DamageInfo dmgInfo, DamageReceiver target, Vector3? srcPos = null, bool noFriendlyFire = true, bool dashingEvades = true, bool simulate = false)
        {
            if (noFriendlyFire && !Team.IsEnemy(target.Team, dmgInfo.fromCharacter?.Team ?? Teams.all)) return false;
            if (dashingEvades)
            {
                var chara = target.health?.TryGetCharacter();
                if (chara != null && chara.Dashing) return false;
            }
            if (!simulate)
            {
                dmgInfo.damagePoint = target.transform.position;
                dmgInfo.damagePoint.y += 0.6f;
                if (srcPos is Vector3 srcVec) dmgInfo.damageNormal = (srcVec - target.transform.position).normalized;
                else dmgInfo.damageNormal = Vector3.up;
                target.Hurt(dmgInfo);
                target.AddBuff(GameplayDataSettings.Buffs.Pain, dmgInfo.fromCharacter);
            }
            return true;
        }
    }
}
