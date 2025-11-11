using System;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC;


namespace ProjectileReflector
{
    public static class ModConfigs
    {
        public static bool ModVersion_1_3 { get => ModConfigEntry.INSTANCE.ModVersion_1_3; }
        public static bool ENABLE_ACTIVE_REFLECT { get => ModConfigEntry.INSTANCE.ENABLE_ACTIVE_REFLECT; }
        public static bool ENABLE_PASSIVE_REFLECT { get => ModConfigEntry.INSTANCE.ENABLE_PASSIVE_REFLECT; }
        public static bool PASSIVE_REFLECT_BY_ADS { get => ModConfigEntry.INSTANCE.PASSIVE_REFLECT_BY_ADS; }
        public static bool PASSIVE_REFLECT_WHEN_RUNNING { get => ModConfigEntry.INSTANCE.PASSIVE_REFLECT_WHEN_RUNNING; }
        public static bool PASSIVE_REFLECT_WHEN_DASHING { get => ModConfigEntry.INSTANCE.PASSIVE_REFLECT_WHEN_DASHING; }
        public static float REFLECT_RANGE { get => ModConfigEntry.INSTANCE.REFLECT_RANGE; }
        public static float REFLECT_RANGE_PASSIVE { get => ModConfigEntry.INSTANCE.REFLECT_RANGE_PASSIVE; }
        public static float TIME_PASSIVE_EXTEND { get => ModConfigEntry.INSTANCE.TIME_PASSIVE_EXTEND; }
        public static float TIME_ACTIVE_EXTEND { get => ModConfigEntry.INSTANCE.TIME_ACTIVE_EXTEND; }
        public static float TIME_SWING_ACTIVE { get => ModConfigEntry.INSTANCE.TIME_SWING_ACTIVE; }
        public static float TIME_ADS_ACTIVE { get => ModConfigEntry.INSTANCE.TIME_ADS_ACTIVE; }
        public static float CHANCE_BACK_ACTIVE { get => ModConfigEntry.INSTANCE.CHANCE_BACK_ACTIVE; }
        public static float CHANCE_BACK_PASSIVE { get => ModConfigEntry.INSTANCE.CHANCE_BACK_PASSIVE; }
        public static float PASSIVE_STAMINA_COST { get => ModConfigEntry.INSTANCE.PASSIVE_STAMINA_COST; }
        public static float ACTIVE_STAMINA_GAIN { get => ModConfigEntry.INSTANCE.ACTIVE_STAMINA_GAIN; }
        public static float DAMAGE_MULT_ACTIVE { get => ModConfigEntry.INSTANCE.DAMAGE_MULT_ACTIVE; }
        public static float DAMAGE_MULT_PASSIVE { get => ModConfigEntry.INSTANCE.DAMAGE_MULT_PASSIVE; }
        public static float DISTANCE_MULT_ACTIVE { get => ModConfigEntry.INSTANCE.DISTANCE_MULT_ACTIVE; }
        public static float DISTANCE_MULT_PASSIVE { get => ModConfigEntry.INSTANCE.DISTANCE_MULT_PASSIVE; }
        public static bool ENABLE_GRENADE_REFLECT { get => ModConfigEntry.INSTANCE.ENABLE_GRENADE_REFLECT; }
        public static bool AIM_GRENADE_OWNER { get => ModConfigEntry.INSTANCE.AIM_GRENADE_OWNER; }
        public static float GRENADE_REFLECT_HORIZONTAL_SPEED { get => ModConfigEntry.INSTANCE.GRENADE_REFLECT_HORIZONTAL_SPEED; }
        public static float GRENADE_REFLECT_VERTICAL_SPEED { get => ModConfigEntry.INSTANCE.GRENADE_REFLECT_VERTICAL_SPEED; }
        public static bool IGNORES_ANGLE { get => ModConfigEntry.INSTANCE.IGNORES_ANGLE; }
        public static bool ACTIVE_CRITICAL { get => ModConfigEntry.INSTANCE.ACTIVE_CRITICAL; }
        public static bool ACTIVE_EXPLOSION { get => ModConfigEntry.INSTANCE.ACTIVE_EXPLOSION; }
        public static float ACTIVE_EXPLOSION_DAMAGE_FACTOR { get => ModConfigEntry.INSTANCE.ACTIVE_EXPLOSION_DAMAGE_FACTOR; }
        public static float ACTIVE_EXPLOSION_RANGE { get => ModConfigEntry.INSTANCE.ACTIVE_EXPLOSION_RANGE; }
        public static bool ENABLES_FLYING_BLADE { get => ModConfigEntry.INSTANCE.ENABLES_FLYING_BLADE; }
        public static float FLYING_BLADE_STRENGTH { get => ModConfigEntry.INSTANCE.FLYING_BLADE_STRENGTH; }
        public static float FLYING_BLADE_VAMPIRISM { get => ModConfigEntry.INSTANCE.FLYING_BLADE_VAMPIRISM; }
        public static float SFX_VOLUME { get => ModConfigEntry.INSTANCE.SFX_VOLUME; }
    }

    [Serializable]
    public partial class ModConfigEntry
    {
        private static ModConfigEntry instance = new ModConfigEntry();
        public bool ModVersion_1_3 = true;
        public bool ENABLE_ACTIVE_REFLECT = true;
        public bool ENABLE_PASSIVE_REFLECT = true;
        public bool PASSIVE_REFLECT_BY_ADS = false;
        public bool PASSIVE_REFLECT_WHEN_RUNNING = false;
        public bool PASSIVE_REFLECT_WHEN_DASHING = false;
        public float REFLECT_RANGE = 2;
        public float REFLECT_RANGE_PASSIVE = 1.5f;
        public float TIME_PASSIVE_EXTEND = 0.1f;
        public float TIME_ACTIVE_EXTEND = 0.2f;
        public float TIME_SWING_ACTIVE = 0.3f;
        public float TIME_ADS_ACTIVE = 0.3f;
        public float CHANCE_BACK_ACTIVE = 0.9f;
        public float CHANCE_BACK_PASSIVE = 0.05f;
        public float PASSIVE_STAMINA_COST = 0.5f;
        public float ACTIVE_STAMINA_GAIN = 5;
        public float DAMAGE_MULT_ACTIVE = 1;
        public float DAMAGE_MULT_PASSIVE = 0.5f;
        public float DISTANCE_MULT_ACTIVE = 5;
        public float DISTANCE_MULT_PASSIVE = 1;
        public bool ENABLE_GRENADE_REFLECT = true;
        public bool AIM_GRENADE_OWNER = true;
        public float GRENADE_REFLECT_HORIZONTAL_SPEED = 10;
        public float GRENADE_REFLECT_VERTICAL_SPEED = 3;
        public bool IGNORES_ANGLE = false;
        public bool ACTIVE_CRITICAL = true;
        public bool ACTIVE_EXPLOSION = false;
        public float ACTIVE_EXPLOSION_DAMAGE_FACTOR = 1;
        public float ACTIVE_EXPLOSION_RANGE = 1;
        public bool ENABLES_FLYING_BLADE = false;
        public float FLYING_BLADE_STRENGTH = 0.5f;
        public float FLYING_BLADE_VAMPIRISM = 0.5f;
        public float SFX_VOLUME = 0.5f;
    }

    // mod config menu compat
    namespace Compat
    {
        public static partial class ModSettingMenu
        {
            public static void Reset()
            {
                if (!ModSettingAPI.IsInit) return;
                var config = ModConfigEntry.INSTANCE;
                ModSettingAPI.SetValue("ModVersion_1_3", config.ModVersion_1_3);
                ModSettingAPI.SetValue("ENABLE_ACTIVE_REFLECT", config.ENABLE_ACTIVE_REFLECT);
                ModSettingAPI.SetValue("ENABLE_PASSIVE_REFLECT", config.ENABLE_PASSIVE_REFLECT);
                ModSettingAPI.SetValue("PASSIVE_REFLECT_BY_ADS", config.PASSIVE_REFLECT_BY_ADS);
                ModSettingAPI.SetValue("PASSIVE_REFLECT_WHEN_RUNNING", config.PASSIVE_REFLECT_WHEN_RUNNING);
                ModSettingAPI.SetValue("PASSIVE_REFLECT_WHEN_DASHING", config.PASSIVE_REFLECT_WHEN_DASHING);
                ModSettingAPI.SetValue("REFLECT_RANGE", config.REFLECT_RANGE);
                ModSettingAPI.SetValue("REFLECT_RANGE_PASSIVE", config.REFLECT_RANGE_PASSIVE);
                ModSettingAPI.SetValue("TIME_PASSIVE_EXTEND", config.TIME_PASSIVE_EXTEND);
                ModSettingAPI.SetValue("TIME_ACTIVE_EXTEND", config.TIME_ACTIVE_EXTEND);
                ModSettingAPI.SetValue("TIME_SWING_ACTIVE", config.TIME_SWING_ACTIVE);
                ModSettingAPI.SetValue("TIME_ADS_ACTIVE", config.TIME_ADS_ACTIVE);
                ModSettingAPI.SetValue("CHANCE_BACK_ACTIVE", config.CHANCE_BACK_ACTIVE);
                ModSettingAPI.SetValue("CHANCE_BACK_PASSIVE", config.CHANCE_BACK_PASSIVE);
                ModSettingAPI.SetValue("PASSIVE_STAMINA_COST", config.PASSIVE_STAMINA_COST);
                ModSettingAPI.SetValue("ACTIVE_STAMINA_GAIN", config.ACTIVE_STAMINA_GAIN);
                ModSettingAPI.SetValue("DAMAGE_MULT_ACTIVE", config.DAMAGE_MULT_ACTIVE);
                ModSettingAPI.SetValue("DAMAGE_MULT_PASSIVE", config.DAMAGE_MULT_PASSIVE);
                ModSettingAPI.SetValue("DISTANCE_MULT_ACTIVE", config.DISTANCE_MULT_ACTIVE);
                ModSettingAPI.SetValue("DISTANCE_MULT_PASSIVE", config.DISTANCE_MULT_PASSIVE);
                ModSettingAPI.SetValue("ENABLE_GRENADE_REFLECT", config.ENABLE_GRENADE_REFLECT);
                ModSettingAPI.SetValue("AIM_GRENADE_OWNER", config.AIM_GRENADE_OWNER);
                ModSettingAPI.SetValue("GRENADE_REFLECT_HORIZONTAL_SPEED", config.GRENADE_REFLECT_HORIZONTAL_SPEED);
                ModSettingAPI.SetValue("GRENADE_REFLECT_VERTICAL_SPEED", config.GRENADE_REFLECT_VERTICAL_SPEED);
                ModSettingAPI.SetValue("IGNORES_ANGLE", config.IGNORES_ANGLE);
                ModSettingAPI.SetValue("ACTIVE_CRITICAL", config.ACTIVE_CRITICAL);
                ModSettingAPI.SetValue("ACTIVE_EXPLOSION", config.ACTIVE_EXPLOSION);
                ModSettingAPI.SetValue("ACTIVE_EXPLOSION_DAMAGE_FACTOR", config.ACTIVE_EXPLOSION_DAMAGE_FACTOR);
                ModSettingAPI.SetValue("ACTIVE_EXPLOSION_RANGE", config.ACTIVE_EXPLOSION_RANGE);
                ModSettingAPI.SetValue("ENABLES_FLYING_BLADE", config.ENABLES_FLYING_BLADE);
                ModSettingAPI.SetValue("FLYING_BLADE_STRENGTH", config.FLYING_BLADE_STRENGTH);
                ModSettingAPI.SetValue("FLYING_BLADE_VAMPIRISM", config.FLYING_BLADE_VAMPIRISM);
                ModSettingAPI.SetValue("SFX_VOLUME", config.SFX_VOLUME);
            }
            static void AddUI(bool isChinese)
            {
                var config = ModConfigEntry.INSTANCE;
                ModSettingAPI.AddToggle(
                    "ModVersion_1_3",
                    isChinese ? "（仅展示）Mod版本：1.3" : "(Display only) Mod version: 1.3",
                    config.ModVersion_1_3,
                    WrapOnChange<bool>(v => config.ModVersion_1_3 = v)
                );
                ModSettingAPI.AddToggle(
                    "ENABLE_ACTIVE_REFLECT",
                    isChinese ? "启用主动反射" : "Enables active reflection",
                    config.ENABLE_ACTIVE_REFLECT,
                    WrapOnChange<bool>(v => config.ENABLE_ACTIVE_REFLECT = v)
                );
                ModSettingAPI.AddToggle(
                    "ENABLE_PASSIVE_REFLECT",
                    isChinese ? "启用被动反射" : "Enables passive reflection",
                    config.ENABLE_PASSIVE_REFLECT,
                    WrapOnChange<bool>(v => config.ENABLE_PASSIVE_REFLECT = v)
                );
                ModSettingAPI.AddToggle(
                    "PASSIVE_REFLECT_BY_ADS",
                    isChinese ? "仅在机瞄状态下启用被动反射" : "Enables passive reflection only during ADS mode",
                    config.PASSIVE_REFLECT_BY_ADS,
                    WrapOnChange<bool>(v => config.PASSIVE_REFLECT_BY_ADS = v)
                );
                ModSettingAPI.AddToggle(
                    "PASSIVE_REFLECT_WHEN_RUNNING",
                    isChinese ? "是否在跑动中被动反射" : "Whether passive reflection enables when running",
                    config.PASSIVE_REFLECT_WHEN_RUNNING,
                    WrapOnChange<bool>(v => config.PASSIVE_REFLECT_WHEN_RUNNING = v)
                );
                ModSettingAPI.AddToggle(
                    "PASSIVE_REFLECT_WHEN_DASHING",
                    isChinese ? "是否在翻滚中被动反射" : "Whether passive reflection enables when dashing",
                    config.PASSIVE_REFLECT_WHEN_DASHING,
                    WrapOnChange<bool>(v => config.PASSIVE_REFLECT_WHEN_DASHING = v)
                );
                ModSettingAPI.AddSlider(
                    "REFLECT_RANGE",
                    isChinese ? "主动反射触发范围" : "Active reflection trigger range",
                    config.REFLECT_RANGE,
                    new Vector2(0, 10),
                    WrapOnChange<float>(v => config.REFLECT_RANGE = v)
                );
                ModSettingAPI.AddSlider(
                    "REFLECT_RANGE_PASSIVE",
                    isChinese ? "被动反射触发范围" : "Passive reflection trigger range",
                    config.REFLECT_RANGE_PASSIVE,
                    new Vector2(0, 10),
                    WrapOnChange<float>(v => config.REFLECT_RANGE_PASSIVE = v)
                );
                ModSettingAPI.AddSlider(
                    "TIME_PASSIVE_EXTEND",
                    isChinese ? "每次被动反射延续状态时长（秒）" : "State duration extension per passive reflection (seconds)",
                    config.TIME_PASSIVE_EXTEND,
                    new Vector2(0, 1),
                    WrapOnChange<float>(v => config.TIME_PASSIVE_EXTEND = v)
                );
                ModSettingAPI.AddSlider(
                    "TIME_ACTIVE_EXTEND",
                    isChinese ? "每次主动反射延续状态时长（秒）" : "State duration extension per active reflection (seconds)",
                    config.TIME_ACTIVE_EXTEND,
                    new Vector2(0, 1),
                    WrapOnChange<float>(v => config.TIME_ACTIVE_EXTEND = v)
                );
                ModSettingAPI.AddSlider(
                    "TIME_SWING_ACTIVE",
                    isChinese ? "挥刀后主动反射状态持续时长（秒）" : "Active reflection state duration after a melee swing (seconds)",
                    config.TIME_SWING_ACTIVE,
                    new Vector2(0, 3),
                    WrapOnChange<float>(v => config.TIME_SWING_ACTIVE = v)
                );
                ModSettingAPI.AddSlider(
                    "TIME_ADS_ACTIVE",
                    isChinese ? "机瞄后主动反射状态持续时长（秒）（需开启“仅机瞄被动反射”）" : "Active reflection state duration after entering ADS mode (seconds) (works only with \"Enables passive during ADS\")",
                    config.TIME_ADS_ACTIVE,
                    new Vector2(0, 3),
                    WrapOnChange<float>(v => config.TIME_ADS_ACTIVE = v)
                );
                ModSettingAPI.AddSlider(
                    "CHANCE_BACK_ACTIVE",
                    isChinese ? "主动反射回弹子弹概率" : "Chance for active reflection to return bullet to shooter",
                    config.CHANCE_BACK_ACTIVE,
                    new Vector2(0, 1),
                    WrapOnChange<float>(v => config.CHANCE_BACK_ACTIVE = v)
                );
                ModSettingAPI.AddSlider(
                    "CHANCE_BACK_PASSIVE",
                    isChinese ? "被动反射回弹子弹概率" : "Chance for passive reflection to return bullet to shooter",
                    config.CHANCE_BACK_PASSIVE,
                    new Vector2(0, 1),
                    WrapOnChange<float>(v => config.CHANCE_BACK_PASSIVE = v)
                );
                ModSettingAPI.AddSlider(
                    "PASSIVE_STAMINA_COST",
                    isChinese ? "被动反射体力消耗率（基于子弹伤害）" : "Passive reflection stamina cost rate (based on bullet damage)",
                    config.PASSIVE_STAMINA_COST,
                    new Vector2(0, 10),
                    WrapOnChange<float>(v => config.PASSIVE_STAMINA_COST = v)
                );
                ModSettingAPI.AddSlider(
                    "ACTIVE_STAMINA_GAIN",
                    isChinese ? "主动反射单颗子弹恢复体力量" : "Stamina amount gain after each single active bullet reflection",
                    config.ACTIVE_STAMINA_GAIN,
                    new Vector2(-10, 100),
                    WrapOnChange<float>(v => config.ACTIVE_STAMINA_GAIN = v)
                );
                ModSettingAPI.AddSlider(
                    "DAMAGE_MULT_ACTIVE",
                    isChinese ? "主动反射后子弹伤害乘数" : "Reflected bullet damage multiplier after active reflection",
                    config.DAMAGE_MULT_ACTIVE,
                    new Vector2(0, 10),
                    WrapOnChange<float>(v => config.DAMAGE_MULT_ACTIVE = v)
                );
                ModSettingAPI.AddSlider(
                    "DAMAGE_MULT_PASSIVE",
                    isChinese ? "被动反射后子弹伤害乘数" : "Reflected bullet damage multiplier after passive reflection",
                    config.DAMAGE_MULT_PASSIVE,
                    new Vector2(0, 10),
                    WrapOnChange<float>(v => config.DAMAGE_MULT_PASSIVE = v)
                );
                ModSettingAPI.AddSlider(
                    "DISTANCE_MULT_ACTIVE",
                    isChinese ? "主动反射后子弹射程乘数" : "Reflected bullet range multiplier after active reflection",
                    config.DISTANCE_MULT_ACTIVE,
                    new Vector2(0, 10),
                    WrapOnChange<float>(v => config.DISTANCE_MULT_ACTIVE = v)
                );
                ModSettingAPI.AddSlider(
                    "DISTANCE_MULT_PASSIVE",
                    isChinese ? "被动反射后子弹射程乘数" : "Reflected bullet range multiplier after passive reflection",
                    config.DISTANCE_MULT_PASSIVE,
                    new Vector2(0, 10),
                    WrapOnChange<float>(v => config.DISTANCE_MULT_PASSIVE = v)
                );
                ModSettingAPI.AddToggle(
                    "ENABLE_GRENADE_REFLECT",
                    isChinese ? "启用手雷反射" : "Enables grenade reflection",
                    config.ENABLE_GRENADE_REFLECT,
                    WrapOnChange<bool>(v => config.ENABLE_GRENADE_REFLECT = v)
                );
                ModSettingAPI.AddToggle(
                    "AIM_GRENADE_OWNER",
                    isChinese ? "反射方向瞄准投掷者（如果非自己）" : "Aims grenade thrower (if not self) at reflection",
                    config.AIM_GRENADE_OWNER,
                    WrapOnChange<bool>(v => config.AIM_GRENADE_OWNER = v)
                );
                ModSettingAPI.AddSlider(
                    "GRENADE_REFLECT_HORIZONTAL_SPEED",
                    isChinese ? "手雷反射横向速度" : "Grenade reflection horizontal speed",
                    config.GRENADE_REFLECT_HORIZONTAL_SPEED,
                    new Vector2(0, 30),
                    WrapOnChange<float>(v => config.GRENADE_REFLECT_HORIZONTAL_SPEED = v)
                );
                ModSettingAPI.AddSlider(
                    "GRENADE_REFLECT_VERTICAL_SPEED",
                    isChinese ? "手雷反射纵向速度" : "Grenade reflection vertical speed",
                    config.GRENADE_REFLECT_VERTICAL_SPEED,
                    new Vector2(0, 15),
                    WrapOnChange<float>(v => config.GRENADE_REFLECT_VERTICAL_SPEED = v)
                );
                ModSettingAPI.AddToggle(
                    "IGNORES_ANGLE",
                    isChinese ? "后方子弹也可反射；或许可解决部分高速子弹穿透防御问题" : "Also reflects bullets from behind; might solve the issue that some high-speed bullets go through the barrier",
                    config.IGNORES_ANGLE,
                    WrapOnChange<bool>(v => config.IGNORES_ANGLE = v)
                );
                ModSettingAPI.AddToggle(
                    "ACTIVE_CRITICAL",
                    isChinese ? "主动反射子弹是否暴击" : "Whether actively reflected bullets are critical hits",
                    config.ACTIVE_CRITICAL,
                    WrapOnChange<bool>(v => config.ACTIVE_CRITICAL = v)
                );
                ModSettingAPI.AddToggle(
                    "ACTIVE_EXPLOSION",
                    isChinese ? "主动反射子弹是否爆炸" : "Whether actively reflected bullets explode",
                    config.ACTIVE_EXPLOSION,
                    WrapOnChange<bool>(v => config.ACTIVE_EXPLOSION = v)
                );
                ModSettingAPI.AddSlider(
                    "ACTIVE_EXPLOSION_DAMAGE_FACTOR",
                    isChinese ? "主动反射爆炸额外伤害乘数（基于反射后子弹伤害）" : "Active reflection explosion extra damage multiplier (based on reflected bullet damage)",
                    config.ACTIVE_EXPLOSION_DAMAGE_FACTOR,
                    new Vector2(0, 10),
                    WrapOnChange<float>(v => config.ACTIVE_EXPLOSION_DAMAGE_FACTOR = v)
                );
                ModSettingAPI.AddSlider(
                    "ACTIVE_EXPLOSION_RANGE",
                    isChinese ? "主动反射爆炸范围" : "Active reflection explosion range",
                    config.ACTIVE_EXPLOSION_RANGE,
                    new Vector2(0, 10),
                    WrapOnChange<float>(v => config.ACTIVE_EXPLOSION_RANGE = v)
                );
                ModSettingAPI.AddToggle(
                    "ENABLES_FLYING_BLADE",
                    isChinese ? "启用飞刃（？）" : "Enables flying blade (?)",
                    config.ENABLES_FLYING_BLADE,
                    WrapOnChange<bool>(v => config.ENABLES_FLYING_BLADE = v)
                );
                ModSettingAPI.AddSlider(
                    "FLYING_BLADE_STRENGTH",
                    isChinese ? "飞刃伤害乘数" : "Flying blade damage multiplier",
                    config.FLYING_BLADE_STRENGTH,
                    new Vector2(0, 5),
                    WrapOnChange<float>(v => config.FLYING_BLADE_STRENGTH = v)
                );
                ModSettingAPI.AddSlider(
                    "FLYING_BLADE_VAMPIRISM",
                    isChinese ? "飞刃造成伤害吸血比例" : "Damage HP absorbing ratio for flying blades",
                    config.FLYING_BLADE_VAMPIRISM,
                    new Vector2(0, 2),
                    WrapOnChange<float>(v => config.FLYING_BLADE_VAMPIRISM = v)
                );
                ModSettingAPI.AddSlider(
                    "SFX_VOLUME",
                    isChinese ? "反射音效强度" : "Reflection sound effect volume",
                    config.SFX_VOLUME,
                    new Vector2(0, 1),
                    WrapOnChange<float>(v => config.SFX_VOLUME = v)
                );
                ModSettingAPI.AddGroup("Version 1.3", "Version 1.3", new List<string>() { "ModVersion_1_3" });
                ModSettingAPI.AddGroup("Functions", "Functions", new List<string>() { "ENABLE_ACTIVE_REFLECT", "ENABLE_PASSIVE_REFLECT", "PASSIVE_REFLECT_BY_ADS", "PASSIVE_REFLECT_WHEN_RUNNING", "PASSIVE_REFLECT_WHEN_DASHING" });
                ModSettingAPI.AddGroup("Parameters", "Parameters", new List<string>() { "REFLECT_RANGE", "REFLECT_RANGE_PASSIVE", "TIME_PASSIVE_EXTEND", "TIME_ACTIVE_EXTEND", "TIME_SWING_ACTIVE", "TIME_ADS_ACTIVE", "CHANCE_BACK_ACTIVE", "CHANCE_BACK_PASSIVE", "PASSIVE_STAMINA_COST", "ACTIVE_STAMINA_GAIN", "DAMAGE_MULT_ACTIVE", "DAMAGE_MULT_PASSIVE", "DISTANCE_MULT_ACTIVE", "DISTANCE_MULT_PASSIVE" });
                ModSettingAPI.AddGroup("Grenade Reflection", "Grenade Reflection", new List<string>() { "ENABLE_GRENADE_REFLECT", "AIM_GRENADE_OWNER", "GRENADE_REFLECT_HORIZONTAL_SPEED", "GRENADE_REFLECT_VERTICAL_SPEED" });
                ModSettingAPI.AddGroup("Misc", "Misc", new List<string>() { "IGNORES_ANGLE", "ACTIVE_CRITICAL", "ACTIVE_EXPLOSION", "ACTIVE_EXPLOSION_DAMAGE_FACTOR", "ACTIVE_EXPLOSION_RANGE", "ENABLES_FLYING_BLADE", "FLYING_BLADE_STRENGTH", "FLYING_BLADE_VAMPIRISM" });
                ModSettingAPI.AddGroup("Sound", "Sound", new List<string>() { "SFX_VOLUME" });
            }
        }
        public static partial class ModConfigMenu
        {
            static void SetupModConfig(bool isChinese)
            {
                var config = ModConfigEntry.INSTANCE;
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "SFX_VOLUME",
                    isChinese ? "反射音效强度" : "Reflection sound effect volume",
                    typeof(float),
                    config.SFX_VOLUME,
                    new Vector2(0, 1)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "FLYING_BLADE_VAMPIRISM",
                    isChinese ? "飞刃造成伤害吸血比例" : "Damage HP absorbing ratio for flying blades",
                    typeof(float),
                    config.FLYING_BLADE_VAMPIRISM,
                    new Vector2(0, 2)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "FLYING_BLADE_STRENGTH",
                    isChinese ? "飞刃伤害乘数" : "Flying blade damage multiplier",
                    typeof(float),
                    config.FLYING_BLADE_STRENGTH,
                    new Vector2(0, 5)
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ENABLES_FLYING_BLADE",
                    isChinese ? "启用飞刃（？）" : "Enables flying blade (?)",
                    config.ENABLES_FLYING_BLADE
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
                    "ACTIVE_EXPLOSION_DAMAGE_FACTOR",
                    isChinese ? "主动反射爆炸额外伤害乘数（基于反射后子弹伤害）" : "Active reflection explosion extra damage multiplier (based on reflected bullet damage)",
                    typeof(float),
                    config.ACTIVE_EXPLOSION_DAMAGE_FACTOR,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ACTIVE_EXPLOSION",
                    isChinese ? "主动反射子弹是否爆炸" : "Whether actively reflected bullets explode",
                    config.ACTIVE_EXPLOSION
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ACTIVE_CRITICAL",
                    isChinese ? "主动反射子弹是否暴击" : "Whether actively reflected bullets are critical hits",
                    config.ACTIVE_CRITICAL
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "IGNORES_ANGLE",
                    isChinese ? "后方子弹也可反射；或许可解决部分高速子弹穿透防御问题" : "Also reflects bullets from behind; might solve the issue that some high-speed bullets go through the barrier",
                    config.IGNORES_ANGLE
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "GRENADE_REFLECT_VERTICAL_SPEED",
                    isChinese ? "手雷反射纵向速度" : "Grenade reflection vertical speed",
                    typeof(float),
                    config.GRENADE_REFLECT_VERTICAL_SPEED,
                    new Vector2(0, 15)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "GRENADE_REFLECT_HORIZONTAL_SPEED",
                    isChinese ? "手雷反射横向速度" : "Grenade reflection horizontal speed",
                    typeof(float),
                    config.GRENADE_REFLECT_HORIZONTAL_SPEED,
                    new Vector2(0, 30)
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "AIM_GRENADE_OWNER",
                    isChinese ? "反射方向瞄准投掷者（如果非自己）" : "Aims grenade thrower (if not self) at reflection",
                    config.AIM_GRENADE_OWNER
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ENABLE_GRENADE_REFLECT",
                    isChinese ? "启用手雷反射" : "Enables grenade reflection",
                    config.ENABLE_GRENADE_REFLECT
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "DISTANCE_MULT_PASSIVE",
                    isChinese ? "被动反射后子弹射程乘数" : "Reflected bullet range multiplier after passive reflection",
                    typeof(float),
                    config.DISTANCE_MULT_PASSIVE,
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
                    "DAMAGE_MULT_PASSIVE",
                    isChinese ? "被动反射后子弹伤害乘数" : "Reflected bullet damage multiplier after passive reflection",
                    typeof(float),
                    config.DAMAGE_MULT_PASSIVE,
                    new Vector2(0, 10)
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
                    "ACTIVE_STAMINA_GAIN",
                    isChinese ? "主动反射单颗子弹恢复体力量" : "Stamina amount gain after each single active bullet reflection",
                    typeof(float),
                    config.ACTIVE_STAMINA_GAIN,
                    new Vector2(-10, 100)
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
                    "CHANCE_BACK_PASSIVE",
                    isChinese ? "被动反射回弹子弹概率" : "Chance for passive reflection to return bullet to shooter",
                    typeof(float),
                    config.CHANCE_BACK_PASSIVE,
                    new Vector2(0, 1)
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
                    "TIME_ADS_ACTIVE",
                    isChinese ? "机瞄后主动反射状态持续时长（秒）（需开启“仅机瞄被动反射”）" : "Active reflection state duration after entering ADS mode (seconds) (works only with \"Enables passive during ADS\")",
                    typeof(float),
                    config.TIME_ADS_ACTIVE,
                    new Vector2(0, 3)
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
                    "TIME_ACTIVE_EXTEND",
                    isChinese ? "每次主动反射延续状态时长（秒）" : "State duration extension per active reflection (seconds)",
                    typeof(float),
                    config.TIME_ACTIVE_EXTEND,
                    new Vector2(0, 1)
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
                    "REFLECT_RANGE_PASSIVE",
                    isChinese ? "被动反射触发范围" : "Passive reflection trigger range",
                    typeof(float),
                    config.REFLECT_RANGE_PASSIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "REFLECT_RANGE",
                    isChinese ? "主动反射触发范围" : "Active reflection trigger range",
                    typeof(float),
                    config.REFLECT_RANGE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "PASSIVE_REFLECT_WHEN_DASHING",
                    isChinese ? "是否在翻滚中被动反射" : "Whether passive reflection enables when dashing",
                    config.PASSIVE_REFLECT_WHEN_DASHING
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "PASSIVE_REFLECT_WHEN_RUNNING",
                    isChinese ? "是否在跑动中被动反射" : "Whether passive reflection enables when running",
                    config.PASSIVE_REFLECT_WHEN_RUNNING
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "PASSIVE_REFLECT_BY_ADS",
                    isChinese ? "仅在机瞄状态下启用被动反射" : "Enables passive reflection only during ADS mode",
                    config.PASSIVE_REFLECT_BY_ADS
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ENABLE_PASSIVE_REFLECT",
                    isChinese ? "启用被动反射" : "Enables passive reflection",
                    config.ENABLE_PASSIVE_REFLECT
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ENABLE_ACTIVE_REFLECT",
                    isChinese ? "启用主动反射" : "Enables active reflection",
                    config.ENABLE_ACTIVE_REFLECT
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ModVersion_1_3",
                    isChinese ? "（仅展示）Mod版本：1.3" : "(Display only) Mod version: 1.3",
                    config.ModVersion_1_3
                );
            }
            static void LoadConfigFromModConfig()
            {
                var config = ModConfigEntry.INSTANCE;
                config.ModVersion_1_3 = ModConfigAPI.SafeLoad(MOD_NAME, "ModVersion_1_3", config.ModVersion_1_3);
                config.ENABLE_ACTIVE_REFLECT = ModConfigAPI.SafeLoad(MOD_NAME, "ENABLE_ACTIVE_REFLECT", config.ENABLE_ACTIVE_REFLECT);
                config.ENABLE_PASSIVE_REFLECT = ModConfigAPI.SafeLoad(MOD_NAME, "ENABLE_PASSIVE_REFLECT", config.ENABLE_PASSIVE_REFLECT);
                config.PASSIVE_REFLECT_BY_ADS = ModConfigAPI.SafeLoad(MOD_NAME, "PASSIVE_REFLECT_BY_ADS", config.PASSIVE_REFLECT_BY_ADS);
                config.PASSIVE_REFLECT_WHEN_RUNNING = ModConfigAPI.SafeLoad(MOD_NAME, "PASSIVE_REFLECT_WHEN_RUNNING", config.PASSIVE_REFLECT_WHEN_RUNNING);
                config.PASSIVE_REFLECT_WHEN_DASHING = ModConfigAPI.SafeLoad(MOD_NAME, "PASSIVE_REFLECT_WHEN_DASHING", config.PASSIVE_REFLECT_WHEN_DASHING);
                config.REFLECT_RANGE = ModConfigAPI.SafeLoad(MOD_NAME, "REFLECT_RANGE", config.REFLECT_RANGE);
                config.REFLECT_RANGE_PASSIVE = ModConfigAPI.SafeLoad(MOD_NAME, "REFLECT_RANGE_PASSIVE", config.REFLECT_RANGE_PASSIVE);
                config.TIME_PASSIVE_EXTEND = ModConfigAPI.SafeLoad(MOD_NAME, "TIME_PASSIVE_EXTEND", config.TIME_PASSIVE_EXTEND);
                config.TIME_ACTIVE_EXTEND = ModConfigAPI.SafeLoad(MOD_NAME, "TIME_ACTIVE_EXTEND", config.TIME_ACTIVE_EXTEND);
                config.TIME_SWING_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "TIME_SWING_ACTIVE", config.TIME_SWING_ACTIVE);
                config.TIME_ADS_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "TIME_ADS_ACTIVE", config.TIME_ADS_ACTIVE);
                config.CHANCE_BACK_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "CHANCE_BACK_ACTIVE", config.CHANCE_BACK_ACTIVE);
                config.CHANCE_BACK_PASSIVE = ModConfigAPI.SafeLoad(MOD_NAME, "CHANCE_BACK_PASSIVE", config.CHANCE_BACK_PASSIVE);
                config.PASSIVE_STAMINA_COST = ModConfigAPI.SafeLoad(MOD_NAME, "PASSIVE_STAMINA_COST", config.PASSIVE_STAMINA_COST);
                config.ACTIVE_STAMINA_GAIN = ModConfigAPI.SafeLoad(MOD_NAME, "ACTIVE_STAMINA_GAIN", config.ACTIVE_STAMINA_GAIN);
                config.DAMAGE_MULT_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "DAMAGE_MULT_ACTIVE", config.DAMAGE_MULT_ACTIVE);
                config.DAMAGE_MULT_PASSIVE = ModConfigAPI.SafeLoad(MOD_NAME, "DAMAGE_MULT_PASSIVE", config.DAMAGE_MULT_PASSIVE);
                config.DISTANCE_MULT_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "DISTANCE_MULT_ACTIVE", config.DISTANCE_MULT_ACTIVE);
                config.DISTANCE_MULT_PASSIVE = ModConfigAPI.SafeLoad(MOD_NAME, "DISTANCE_MULT_PASSIVE", config.DISTANCE_MULT_PASSIVE);
                config.ENABLE_GRENADE_REFLECT = ModConfigAPI.SafeLoad(MOD_NAME, "ENABLE_GRENADE_REFLECT", config.ENABLE_GRENADE_REFLECT);
                config.AIM_GRENADE_OWNER = ModConfigAPI.SafeLoad(MOD_NAME, "AIM_GRENADE_OWNER", config.AIM_GRENADE_OWNER);
                config.GRENADE_REFLECT_HORIZONTAL_SPEED = ModConfigAPI.SafeLoad(MOD_NAME, "GRENADE_REFLECT_HORIZONTAL_SPEED", config.GRENADE_REFLECT_HORIZONTAL_SPEED);
                config.GRENADE_REFLECT_VERTICAL_SPEED = ModConfigAPI.SafeLoad(MOD_NAME, "GRENADE_REFLECT_VERTICAL_SPEED", config.GRENADE_REFLECT_VERTICAL_SPEED);
                config.IGNORES_ANGLE = ModConfigAPI.SafeLoad(MOD_NAME, "IGNORES_ANGLE", config.IGNORES_ANGLE);
                config.ACTIVE_CRITICAL = ModConfigAPI.SafeLoad(MOD_NAME, "ACTIVE_CRITICAL", config.ACTIVE_CRITICAL);
                config.ACTIVE_EXPLOSION = ModConfigAPI.SafeLoad(MOD_NAME, "ACTIVE_EXPLOSION", config.ACTIVE_EXPLOSION);
                config.ACTIVE_EXPLOSION_DAMAGE_FACTOR = ModConfigAPI.SafeLoad(MOD_NAME, "ACTIVE_EXPLOSION_DAMAGE_FACTOR", config.ACTIVE_EXPLOSION_DAMAGE_FACTOR);
                config.ACTIVE_EXPLOSION_RANGE = ModConfigAPI.SafeLoad(MOD_NAME, "ACTIVE_EXPLOSION_RANGE", config.ACTIVE_EXPLOSION_RANGE);
                config.ENABLES_FLYING_BLADE = ModConfigAPI.SafeLoad(MOD_NAME, "ENABLES_FLYING_BLADE", config.ENABLES_FLYING_BLADE);
                config.FLYING_BLADE_STRENGTH = ModConfigAPI.SafeLoad(MOD_NAME, "FLYING_BLADE_STRENGTH", config.FLYING_BLADE_STRENGTH);
                config.FLYING_BLADE_VAMPIRISM = ModConfigAPI.SafeLoad(MOD_NAME, "FLYING_BLADE_VAMPIRISM", config.FLYING_BLADE_VAMPIRISM);
                config.SFX_VOLUME = ModConfigAPI.SafeLoad(MOD_NAME, "SFX_VOLUME", config.SFX_VOLUME);
            }
        }
    }
}
