using UnityEngine;

namespace YukkuriC.AlienGuns.Ext
{
    public static class MathExt
    {
        public static Vector3 RotateY(this Vector3 vec, float degree) => Quaternion.Euler(0, degree, 0) * vec;
    }
}
 