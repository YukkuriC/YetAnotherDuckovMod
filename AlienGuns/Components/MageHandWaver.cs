using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace YukkuriC.AlienGuns.Components
{
    public class MageHandWaver : MonoBehaviour
    {
        static float center = 70, size = 10, timeScale = 5;

        public Transform arm1, arm2;
        void Update()
        {
            var t = Time.timeSinceLevelLoad;
            var angle = center + Mathf.Sin(t * timeScale) * size;
            ApplyAngleX(angle, arm1);
            ApplyAngleX(angle, arm2);
        }
        void ApplyAngleX(float angle, Transform arm)
        {
            if (arm == null) return;
            var oldAngle = arm.localEulerAngles;
            oldAngle.x = angle;
            arm.localEulerAngles = oldAngle;
        }
    }
}
