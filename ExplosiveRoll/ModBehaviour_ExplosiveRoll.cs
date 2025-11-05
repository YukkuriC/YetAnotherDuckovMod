using HarmonyLib;
using Saves;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ExplosiveRoll
{
    [HarmonyPatch]
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        public const string HARMONY_ID = "YukkuriC.ExplosiveRoll";
        public static readonly string ROOT_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        Harmony? patcher;
        void OnEnable()
        {
            if (patcher == null) patcher = new Harmony(HARMONY_ID);
            patcher.PatchAll();
            Debug.Log("[ExplosiveRoll] Harmony patched");
        }
        void OnDisable()
        {
            patcher?.UnpatchAll(HARMONY_ID);
            Debug.Log("[ExplosiveRoll] Harmony released");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CharacterMainControl), nameof(CharacterMainControl.Dash))]
        static void BoomAfterDash(CharacterMainControl __instance)
        {
            var self = __instance;
            if (!self.IsMainCharacter || !self.Dashing) return;
            var explosionRange = 7;
            var damageInfo = new DamageInfo(self)
            {
                damageValue = 50,
                fromWeaponItemID = -1,
                armorPiercing = 6,
            };
            LevelManager.Instance.ExplosionManager.CreateExplosion(self.transform.position, explosionRange, damageInfo, ExplosionFxTypes.flash, 1f, true);

            self.StartCoroutine(DelayCall(0.4f, () =>
            {
                if (self == null) return;
                var expandRadius = 8;
                var expandAngle = 60;
                var delta = self.CurrentMoveDirection * expandRadius;
                for (var i = 0; i < 360; i += expandAngle)
                {
                    var r = i * Mathf.PI / 180;
                    var newCenter = self.transform.position +
                        new Vector3(
                            delta.x * Mathf.Cos(r) - delta.z * Mathf.Sin(r),
                            delta.y,
                            delta.z * Mathf.Cos(r) + delta.x * Mathf.Sin(r)
                        );
                    LevelManager.Instance.ExplosionManager.CreateExplosion(newCenter, explosionRange, damageInfo, ExplosionFxTypes.normal, 1f, true);
                }
            }));
        }

        static IEnumerator DelayCall(float delay, Action next)
        {
            yield return new WaitForSeconds(delay);
            next();
        }
    }
}