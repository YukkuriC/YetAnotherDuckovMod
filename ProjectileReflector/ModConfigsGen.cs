using System;
using UnityEngine;

namespace ProjectileReflector
{
    public static class ModConfigs
    {
        public static float REFLECT_RANGE { get => ModConfigEntry.INSTANCE.REFLECT_RANGE; }
        public static float REFLECT_RANGE_PASSIVE { get => ModConfigEntry.INSTANCE.REFLECT_RANGE_PASSIVE; }
        public static float TIME_PASSIVE_EXTEND { get => ModConfigEntry.INSTANCE.TIME_PASSIVE_EXTEND; }
        public static float TIME_ACTIVE_EXTEND { get => ModConfigEntry.INSTANCE.TIME_ACTIVE_EXTEND; }
        public static float TIME_SWING_ACTIVE { get => ModConfigEntry.INSTANCE.TIME_SWING_ACTIVE; }
        public static float CHANCE_BACK_ACTIVE { get => ModConfigEntry.INSTANCE.CHANCE_BACK_ACTIVE; }
        public static float CHANCE_BACK_PASSIVE { get => ModConfigEntry.INSTANCE.CHANCE_BACK_PASSIVE; }
        public static float PASSIVE_STAMINA_COST { get => ModConfigEntry.INSTANCE.PASSIVE_STAMINA_COST; }
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
        public float REFLECT_RANGE = 2;
        public float REFLECT_RANGE_PASSIVE = 1.5f;
        public float TIME_PASSIVE_EXTEND = 0.1f;
        public float TIME_ACTIVE_EXTEND = 0.2f;
        public float TIME_SWING_ACTIVE = 0.3f;
        public float CHANCE_BACK_ACTIVE = 0.9f;
        public float CHANCE_BACK_PASSIVE = 0.05f;
        public float PASSIVE_STAMINA_COST = 0.5f;
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
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "REFLECT_RANGE",
                    "REFLECT_RANGE",
                    typeof(float),
                    config.REFLECT_RANGE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "REFLECT_RANGE_PASSIVE",
                    "REFLECT_RANGE_PASSIVE",
                    typeof(float),
                    config.REFLECT_RANGE_PASSIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "TIME_PASSIVE_EXTEND",
                    "TIME_PASSIVE_EXTEND",
                    typeof(float),
                    config.TIME_PASSIVE_EXTEND,
                    new Vector2(0, 1)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "TIME_ACTIVE_EXTEND",
                    "TIME_ACTIVE_EXTEND",
                    typeof(float),
                    config.TIME_ACTIVE_EXTEND,
                    new Vector2(0, 1)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "TIME_SWING_ACTIVE",
                    "TIME_SWING_ACTIVE",
                    typeof(float),
                    config.TIME_SWING_ACTIVE,
                    new Vector2(0, 3)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "CHANCE_BACK_ACTIVE",
                    "CHANCE_BACK_ACTIVE",
                    typeof(float),
                    config.CHANCE_BACK_ACTIVE,
                    new Vector2(0, 1)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "CHANCE_BACK_PASSIVE",
                    "CHANCE_BACK_PASSIVE",
                    typeof(float),
                    config.CHANCE_BACK_PASSIVE,
                    new Vector2(0, 1)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "PASSIVE_STAMINA_COST",
                    "PASSIVE_STAMINA_COST",
                    typeof(float),
                    config.PASSIVE_STAMINA_COST,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "DAMAGE_MULT_ACTIVE",
                    "DAMAGE_MULT_ACTIVE",
                    typeof(float),
                    config.DAMAGE_MULT_ACTIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "DAMAGE_MULT_PASSIVE",
                    "DAMAGE_MULT_PASSIVE",
                    typeof(float),
                    config.DAMAGE_MULT_PASSIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "DISTANCE_MULT_ACTIVE",
                    "DISTANCE_MULT_ACTIVE",
                    typeof(float),
                    config.DISTANCE_MULT_ACTIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "DISTANCE_MULT_PASSIVE",
                    "DISTANCE_MULT_PASSIVE",
                    typeof(float),
                    config.DISTANCE_MULT_PASSIVE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ACTIVE_CRITICAL",
                    "ACTIVE_CRITICAL",
                    config.ACTIVE_CRITICAL
                );
                ModConfigAPI.SafeAddBoolDropdownList(
                    MOD_NAME,
                    "ACTIVE_EXPLOSION",
                    "ACTIVE_EXPLOSION",
                    config.ACTIVE_EXPLOSION
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "ACTIVE_EXPLOSION_DAMAGE_FACTOR",
                    "ACTIVE_EXPLOSION_DAMAGE_FACTOR",
                    typeof(float),
                    config.ACTIVE_EXPLOSION_DAMAGE_FACTOR,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "ACTIVE_EXPLOSION_RANGE",
                    "ACTIVE_EXPLOSION_RANGE",
                    typeof(float),
                    config.ACTIVE_EXPLOSION_RANGE,
                    new Vector2(0, 10)
                );
                ModConfigAPI.SafeAddInputWithSlider(
                    MOD_NAME,
                    "SFX_VOLUME",
                    "SFX_VOLUME",
                    typeof(float),
                    config.SFX_VOLUME,
                    new Vector2(0, 1)
                );
            }
            static void LoadConfigFromModConfig()
            {
                var config = ModConfigEntry.INSTANCE;
                config.REFLECT_RANGE = ModConfigAPI.SafeLoad(MOD_NAME, "REFLECT_RANGE", config.REFLECT_RANGE);
                config.REFLECT_RANGE_PASSIVE = ModConfigAPI.SafeLoad(MOD_NAME, "REFLECT_RANGE_PASSIVE", config.REFLECT_RANGE_PASSIVE);
                config.TIME_PASSIVE_EXTEND = ModConfigAPI.SafeLoad(MOD_NAME, "TIME_PASSIVE_EXTEND", config.TIME_PASSIVE_EXTEND);
                config.TIME_ACTIVE_EXTEND = ModConfigAPI.SafeLoad(MOD_NAME, "TIME_ACTIVE_EXTEND", config.TIME_ACTIVE_EXTEND);
                config.TIME_SWING_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "TIME_SWING_ACTIVE", config.TIME_SWING_ACTIVE);
                config.CHANCE_BACK_ACTIVE = ModConfigAPI.SafeLoad(MOD_NAME, "CHANCE_BACK_ACTIVE", config.CHANCE_BACK_ACTIVE);
                config.CHANCE_BACK_PASSIVE = ModConfigAPI.SafeLoad(MOD_NAME, "CHANCE_BACK_PASSIVE", config.CHANCE_BACK_PASSIVE);
                config.PASSIVE_STAMINA_COST = ModConfigAPI.SafeLoad(MOD_NAME, "PASSIVE_STAMINA_COST", config.PASSIVE_STAMINA_COST);
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