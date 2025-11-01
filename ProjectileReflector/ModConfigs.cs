using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectileReflector
{
    public static class ModConfigs
    {
        public static float REFLECT_RANGE_SQR = 2 * 2;
        public static float REFLECT_RANGE_SQR_PASSIVE = 1.5f * 1.5f;

        public static float TIME_PASSIVE_EXTEND = 0.1f;
        public static float TIME_ACTIVE_EXTEND = 0.2f;
        public static float TIME_SWING_ACTIVE = 0.3f;
        public static float TIME_AIM_ACTIVE = 0.2f;

        public static float CHANCE_BACK_ACTIVE = 0.9f;
        public static float CHANCE_BACK_PASSIVE = 0.05f;
        public static float PASSIVE_STAMINA_COST = 0.5f;

        public static float DAMAGE_MULT_ACTIVE = 1;
        public static float DAMAGE_MULT_PASSIVE = 0.5f;
        public static float DISTANCE_MULT_ACTIVE = 5;
        public static float DISTANCE_MULT_PASSIVE = 1;

        public static bool ACTIVE_EXPLOSION = false;
        public static float ACTIVE_EXPLOSION_DAMAGE_FACTOR = 1;
    }
}
