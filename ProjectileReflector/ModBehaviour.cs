using HarmonyLib;
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
            patcher?.UnpatchSelf();
            Debug.Log("[ProjectileReflector] Harmony released");
        }
    }
}