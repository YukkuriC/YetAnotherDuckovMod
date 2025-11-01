using System;

namespace ProjectileReflector
{
    public static class ModConfigs
    {
        public static float REFLECT_RANGE_SQR { get => ModConfigEntry.INSTANCE.REFLECT_RANGE_SQR; }
        public static float REFLECT_RANGE_SQR_PASSIVE { get => ModConfigEntry.INSTANCE.REFLECT_RANGE_SQR_PASSIVE; }
        public static float TIME_PASSIVE_EXTEND { get => ModConfigEntry.INSTANCE.TIME_PASSIVE_EXTEND; }
        public static float TIME_ACTIVE_EXTEND { get => ModConfigEntry.INSTANCE.TIME_ACTIVE_EXTEND; }
        public static float TIME_SWING_ACTIVE { get => ModConfigEntry.INSTANCE.TIME_SWING_ACTIVE; }
        public static float TIME_AIM_ACTIVE { get => ModConfigEntry.INSTANCE.TIME_AIM_ACTIVE; }
        public static float CHANCE_BACK_ACTIVE { get => ModConfigEntry.INSTANCE.CHANCE_BACK_ACTIVE; }
        public static float CHANCE_BACK_PASSIVE { get => ModConfigEntry.INSTANCE.CHANCE_BACK_PASSIVE; }
        public static float PASSIVE_STAMINA_COST { get => ModConfigEntry.INSTANCE.PASSIVE_STAMINA_COST; }
        public static float DAMAGE_MULT_ACTIVE { get => ModConfigEntry.INSTANCE.DAMAGE_MULT_ACTIVE; }
        public static float DAMAGE_MULT_PASSIVE { get => ModConfigEntry.INSTANCE.DAMAGE_MULT_PASSIVE; }
        public static float DISTANCE_MULT_ACTIVE { get => ModConfigEntry.INSTANCE.DISTANCE_MULT_ACTIVE; }
        public static float DISTANCE_MULT_PASSIVE { get => ModConfigEntry.INSTANCE.DISTANCE_MULT_PASSIVE; }
        public static bool ACTIVE_EXPLOSION { get => ModConfigEntry.INSTANCE.ACTIVE_EXPLOSION; }
        public static float ACTIVE_EXPLOSION_DAMAGE_FACTOR { get => ModConfigEntry.INSTANCE.ACTIVE_EXPLOSION_DAMAGE_FACTOR; }
        public static float ACTIVE_EXPLOSION_RANGE { get => ModConfigEntry.INSTANCE.ACTIVE_EXPLOSION_RANGE; }
        public static float SFX_VOLUME { get => ModConfigEntry.INSTANCE.SFX_VOLUME; }
    }

    [Serializable]
    public partial class ModConfigEntry
    {
        private static ModConfigEntry instance = new ModConfigEntry();
        public float REFLECT_RANGE_SQR = 2 * 2;
        public float REFLECT_RANGE_SQR_PASSIVE = 1.5f * 1.5f;
        public float TIME_PASSIVE_EXTEND = 0.1f;
        public float TIME_ACTIVE_EXTEND = 0.2f;
        public float TIME_SWING_ACTIVE = 0.3f;
        public float TIME_AIM_ACTIVE = 0.2f;
        public float CHANCE_BACK_ACTIVE = 0.9f;
        public float CHANCE_BACK_PASSIVE = 0.05f;
        public float PASSIVE_STAMINA_COST = 0.5f;
        public float DAMAGE_MULT_ACTIVE = 1;
        public float DAMAGE_MULT_PASSIVE = 0.5f;
        public float DISTANCE_MULT_ACTIVE = 5;
        public float DISTANCE_MULT_PASSIVE = 1;
        public bool ACTIVE_EXPLOSION = false;
        public float ACTIVE_EXPLOSION_DAMAGE_FACTOR = 1;
        public float ACTIVE_EXPLOSION_RANGE = 1;
        public float SFX_VOLUME = 0.5f;
    }
}
