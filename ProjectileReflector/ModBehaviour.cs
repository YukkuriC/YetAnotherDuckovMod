using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ProjectileReflector
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        public const string HARMONY_ID = "YukkuriC.ProjectileReflector";

        Harmony? patcher;
        void OnEnable()
        {
            if (patcher == null) patcher = new Harmony(HARMONY_ID);
            patcher.PatchAll(Assembly.GetExecutingAssembly());
            Debug.Log("[ProjectileReflector] Harmony patched");
        }
        void OnDisable()
        {
            patcher?.UnpatchAll(HARMONY_ID);
            Debug.Log("[ProjectileReflector] Harmony released");
        }
        void Awake()
        {
            var harmonyPath = Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "0Harmony.dll");
            Assembly.LoadFrom(harmonyPath);
        }
    }
}