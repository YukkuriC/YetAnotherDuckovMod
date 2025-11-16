using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ProjectileReflector
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        public const string HARMONY_ID = "YukkuriC.ProjectileReflector";
        public static readonly string ROOT_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static Assembly manualLoadCommonLib = Assembly.LoadFrom(Path.Combine(ROOT_PATH, "CommonLib.dll"));
        private static Assembly manualLoadHarmony = Assembly.LoadFrom(Path.Combine(ROOT_PATH, "0Harmony.dll"));

        Harmony? patcher;
        void OnEnable()
        {
            if (patcher == null) patcher = new Harmony(HARMONY_ID);
            patcher.PatchAll();
            Debug.Log("[ProjectileReflector] Harmony patched");
            Compat.ModConfigMenu.Init();
        }
        protected override void OnAfterSetup()
        {
            Compat.ModSettingMenu.Init(info);
        }
        void OnDisable()
        {
            patcher?.UnpatchAll(HARMONY_ID);
            Debug.Log("[ProjectileReflector] Harmony released");
            Compat.ModConfigMenu.Dispose();
            Compat.ModSettingMenu.Dispose();
        }
        void Awake()
        {
            // seems it auto loads DLLs next to it
            ModAudio.InitAudio();
            ModConfigEntry.Init();
            if (!SteamManager.Initialized)
            {
                LevelManager.enemySpawnCountFactor = 100;
            }
        }
        void LateUpdate()
        {
            ReflectController.DoReflectGrenade = false;
        }
    }
}
