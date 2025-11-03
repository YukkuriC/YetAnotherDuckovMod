using System;
using UnityEngine;
using YukkuriC;

namespace ProjectileReflector
{
    public static class ModConfigs
    {
        public static bool ENABLE_ACTIVE_REFLECT { get => ModConfigEntry.INSTANCE.ENABLE_ACTIVE_REFLECT; }
        public static bool ENABLE_PASSIVE_REFLECT { get => ModConfigEntry.INSTANCE.ENABLE_PASSIVE_REFLECT; }
        public static float REFLECT_RANGE { get => ModConfigEntry.INSTANCE.REFLECT_RANGE; }
        public static float REFLECT_RANGE_PASSIVE { get => ModConfigEntry.INSTANCE.REFLECT_RANGE_PASSIVE; }
        public static float TIME_PASSIVE_EXTEND { get => ModConfigEntry.INSTANCE.TIME_PASSIVE_EXTEND; }
        public static float TIME_ACTIVE_EXTEND { get => ModConfigEntry.INSTANCE.TIME_ACTIVE_EXTEND; }
        public static float TIME_SWING_ACTIVE { get => ModConfigEntry.INSTANCE.TIME_SWING_ACTIVE; }
        public static float CHANCE_BACK_ACTIVE { get => ModConfigEntry.INSTANCE.CHANCE_BACK_ACTIVE; }
        public static float CHANCE_BACK_PASSIVE { get => ModConfigEntry.INSTANCE.CHANCE_BACK_PASSIVE; }
        public static float PASSIVE_STAMINA_COST { get => ModConfigEntry.INSTANCE.PASSIVE_STAMINA_COST; }
        public static float ACTIVE_STAMINA_GAIN { get => ModConfigEntry.INSTANCE.ACTIVE_STAMINA_GAIN; }
        public static float DAMAGE_MULT_ACTIVE { get => ModConfigEntry.INSTANCE.DAMAGE_MULT_ACTIVE; }
        public static float DAMAGE_MULT_PASSIVE { get => ModConfigEntry.INSTANCE.DAMAGE_MULT_PASSIVE; }
        public static float DISTANCE_MULT_ACTIVE { get => ModConfigEntry.INSTANCE.DISTANCE_MULT_ACTIVE; }
        public static float DISTANCE_MULT_PASSIVE { get => ModConfigEntry.INSTANCE.DISTANCE_MULT_PASSIVE; }
        public static bool ACTIVE_CRITICAL { get => ModConfigEntry.INSTANCE.ACTIVE_CRITICAL; }
        public static bool ACTIVE_EXPLOSION { get => ModConfigEntry.INSTANCE.ACTIVE_EXPLOSION; }
        public static float ACTIVE_EXPLOSION_DAMAGE_FACTOR { get => ModConfigEntry.INSTANCE.ACTIVE_EXPLOSION_DAMAGE_FACTOR; }
        public static float ACTIVE_EXPLOSION_RANGE { get => ModConfigEntry.INSTANCE.ACTIVE_EXPLOSION_RANGE; }
        public static float SFX_VOLUME { get => ModConfigEntry.INSTANCE.SFX_VOLUME; }
    }

    [Serializable]
    public partial class ModConfigEntry
    {
        private static ModConfigEntry instance = new ModConfigEntry();
        public bool ENABLE_ACTIVE_REFLECT = true;
        public bool ENABLE_PASSIVE_REFLECT = true;
        public float REFLECT_RANGE = 2;
        public float REFLECT_RANGE_PASSIVE = 1.5f;
        public float TIME_PASSIVE_EXTEND = 0.1f;
        public float TIME_ACTIVE_EXTEND = 0.2f;
        public float TIME_SWING_ACTIVE = 0.3f;
        public float CHANCE_BACK_ACTIVE = 0.9f;
        public float CHANCE_BACK_PASSIVE = 0.05f;
        public float PASSIVE_STAMINA_COST = 0.5f;
        public float ACTIVE_STAMINA_GAIN = 5;
        public float DAMAGE_MULT_ACTIVE = 1;
        public float DAMAGE_MULT_PASSIVE = 0.5f;
        public float DISTANCE_MULT_ACTIVE = 5;
        public float DISTANCE_MULT_PASSIVE = 1;
        public bool ACTIVE_CRITICAL = true;
        public bool ACTIVE_EXPLOSION = false;
        public float ACTIVE_EXPLOSION_DAMAGE_FACTOR = 1;
        public float ACTIVE_EXPLOSION_RANGE = 1;
        public float SFX_VOLUME = 0.5f;
    }
    // mod config menu compat
    namespace Compat
    {
        public static partial class ModConfigMenu
        {
            static void SetupModConfig(bool isChinese)
            {
                var config = ModConfigEntry.INSTANCE;
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ENABLE_ACTIVE_REFLECT",
                    isChinese ? "启用主动反射" : "Enables active reflection",
                    config.ENABLE_ACTIVE_REFLECT
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ENABLE_PASSIVE_REFLECT",
                    isChinese ? "启用被动反射" : "Enables passive reflection",
                    config.ENABLE_PASSIVE_REFLECT
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "REFLECT_RANGE",
                    isChinese ? "主动反射触发范围" : "Active reflection trigger range",
                    typeof(float),
                    config.REFLECT_RANGE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "REFLECT_RANGE_PASSIVE",
                    isChinese ? "被动反射触发范围" : "Passive reflection trigger range",
                    typeof(float),
                    config.REFLECT_RANGE_PASSIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "TIME_PASSIVE_EXTEND",
                    isChinese ? "每次被动反射延续状态时长（秒）" : "State duration extension per passive reflection (seconds)",
                    typeof(float),
                    config.TIME_PASSIVE_EXTEND,
                    new Vector2(0, 1)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "TIME_ACTIVE_EXTEND",
                    isChinese ? "每次主动反射延续状态时长（秒）" : "State duration extension per active reflection (seconds)",
                    typeof(float),
                    config.TIME_ACTIVE_EXTEND,
                    new Vector2(0, 1)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "TIME_SWING_ACTIVE",
                    isChinese ? "挥刀后主动反射状态持续时长（秒）" : "Active reflection state duration after a melee swing (seconds)",
                    typeof(float),
                    config.TIME_SWING_ACTIVE,
                    new Vector2(0, 3)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "CHANCE_BACK_ACTIVE",
                    isChinese ? "主动反射回弹子弹概率" : "Chance for active reflection to return bullet to shooter",
                    typeof(float),
                    config.CHANCE_BACK_ACTIVE,
                    new Vector2(0, 1)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "CHANCE_BACK_PASSIVE",
                    isChinese ? "被动反射回弹子弹概率" : "Chance for passive reflection to return bullet to shooter",
                    typeof(float),
                    config.CHANCE_BACK_PASSIVE,
                    new Vector2(0, 1)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "PASSIVE_STAMINA_COST",
                    isChinese ? "被动反射体力消耗率（基于子弹伤害）" : "Passive reflection stamina cost rate (based on bullet damage)",
                    typeof(float),
                    config.PASSIVE_STAMINA_COST,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "ACTIVE_STAMINA_GAIN",
                    isChinese ? "主动反射单颗子弹恢复体力量" : "Stamina amount gain after each single active bullet reflection",
                    typeof(float),
                    config.ACTIVE_STAMINA_GAIN,
                    new Vector2(0, 100)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "DAMAGE_MULT_ACTIVE",
                    isChinese ? "主动反射后子弹伤害乘数" : "Reflected bullet damage multiplier after active reflection",
                    typeof(float),
                    config.DAMAGE_MULT_ACTIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "DAMAGE_MULT_PASSIVE",
                    isChinese ? "被动反射后子弹伤害乘数" : "Reflected bullet damage multiplier after passive reflection",
                    typeof(float),
                    config.DAMAGE_MULT_PASSIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "DISTANCE_MULT_ACTIVE",
                    isChinese ? "主动反射后子弹射程乘数" : "Reflected bullet range multiplier after active reflection",
                    typeof(float),
                    config.DISTANCE_MULT_ACTIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "DISTANCE_MULT_PASSIVE",
                    isChinese ? "被动反射后子弹射程乘数" : "Reflected bullet range multiplier after passive reflection",
                    typeof(float),
                    config.DISTANCE_MULT_PASSIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ACTIVE_CRITICAL",
                    isChinese ? "主动反射子弹是否暴击" : "Whether actively reflected bullets are critical hits",
                    config.ACTIVE_CRITICAL
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ACTIVE_EXPLOSION",
                    isChinese ? "主动反射子弹是否爆炸" : "Whether actively reflected bullets explode",
                    config.ACTIVE_EXPLOSION
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "ACTIVE_EXPLOSION_DAMAGE_FACTOR",
                    isChinese ? "主动反射爆炸额外伤害乘数（基于反射后子弹伤害）" : "Active reflection explosion extra damage multiplier (based on reflected bullet damage)",
                    typeof(float),
                    config.ACTIVE_EXPLOSION_DAMAGE_FACTOR,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "ACTIVE_EXPLOSION_RANGE",
                    isChinese ? "主动反射爆炸范围" : "Active reflection explosion range",
                    typeof(float),
                    config.ACTIVE_EXPLOSION_RANGE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "SFX_VOLUME",
                    isChinese ? "反射音效强度" : "Reflection sound effect volume",
                    typeof(float),
                    config.SFX_VOLUME,
                    new Vector2(0, 1)
                );
            }
            static void LoadConfigFromModConfig()
            {
                var config = ModConfigEntry.INSTANCE;
                config.ENABLE_ACTIVE_REFLECT = ModConfigAPI.SafeLoad(MOD_NAME, "ENABLE_ACTIVE_REFLECT", config.ENABLE_ACTIVE_REFLECT);
                config.ENABLE_PASSIVE_REFLECT = ModConfigAPI.SafeLoad(MOD_NAME, "ENABLE_PASSIVE_REFLECT", config.ENABLE_PASSIVE_REFLECT);
                config.REFLECT_RANGE = ModConfigAPI.SafeLoad(MOD_NAME, "REFLECT_RANGE", config.REFLECT_RANGE);
                config.REFLECT_RANGE_PASSIVE = ModConfigAPI.SafeLoad(MOD_NAME, "REFLECT_RANGE_PASSIVE", config.REFLECT_RANGE_PASSIVE);
                config.TIME_PASSIVE_EXTEND = ModConfigAPI.SafeLoad(MOD_NAME, "TIME_PASSIVE_EXTEND", config.TIME_PASSIVE_EXTEND);
                config.TIME_ACTIVE_EXTEND = ModConfigAPI.SafeLoad(MOD_NAME, "TIME_ACTIVE_EXTEND", config.TIME_ACTIVE_EXTEND);
                config.TIME_SWING_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "TIME_SWING_ACTIVE", config.TIME_SWING_ACTIVE);
                config.CHANCE_BACK_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "CHANCE_BACK_ACTIVE", config.CHANCE_BACK_ACTIVE);
                config.CHANCE_BACK_PASSIVE = ModConfigAPI.SafeLoad(MOD_NAME, "CHANCE_BACK_PASSIVE", config.CHANCE_BACK_PASSIVE);
                config.PASSIVE_STAMINA_COST = ModConfigAPI.SafeLoad(MOD_NAME, "PASSIVE_STAMINA_COST", config.PASSIVE_STAMINA_COST);
                config.ACTIVE_STAMINA_GAIN = ModConfigAPI.SafeLoad(MOD_NAME, "ACTIVE_STAMINA_GAIN", config.ACTIVE_STAMINA_GAIN);
                config.DAMAGE_MULT_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "DAMAGE_MULT_ACTIVE", config.DAMAGE_MULT_ACTIVE);
                config.DAMAGE_MULT_PASSIVE = ModConfigAPI.SafeLoad(MOD_NAME, "DAMAGE_MULT_PASSIVE", config.DAMAGE_MULT_PASSIVE);
                config.DISTANCE_MULT_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "DISTANCE_MULT_ACTIVE", config.DISTANCE_MULT_ACTIVE);
                config.DISTANCE_MULT_PASSIVE = ModConfigAPI.SafeLoad(MOD_NAME, "DISTANCE_MULT_PASSIVE", config.DISTANCE_MULT_PASSIVE);
                config.ACTIVE_CRITICAL = ModConfigAPI.SafeLoad(MOD_NAME, "ACTIVE_CRITICAL", config.ACTIVE_CRITICAL);
                config.ACTIVE_EXPLOSION = ModConfigAPI.SafeLoad(MOD_NAME, "ACTIVE_EXPLOSION", config.ACTIVE_EXPLOSION);
                config.ACTIVE_EXPLOSION_DAMAGE_FACTOR = ModConfigAPI.SafeLoad(MOD_NAME, "ACTIVE_EXPLOSION_DAMAGE_FACTOR", config.ACTIVE_EXPLOSION_DAMAGE_FACTOR);
                config.ACTIVE_EXPLOSION_RANGE = ModConfigAPI.SafeLoad(MOD_NAME, "ACTIVE_EXPLOSION_RANGE", config.ACTIVE_EXPLOSION_RANGE);
                config.SFX_VOLUME = ModConfigAPI.SafeLoad(MOD_NAME, "SFX_VOLUME", config.SFX_VOLUME);
            }
        }
    }
}