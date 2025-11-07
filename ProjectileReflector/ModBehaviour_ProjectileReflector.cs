using HarmonyLib;
using Saves;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ProjectileReflector
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        public const string HARMONY_ID = "YukkuriC.ProjectileReflector";
        public static readonly string ROOT_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        Harmony? patcher;
        void OnEnable()
        {
            if (patcher == null) patcher = new Harmony(HARMONY_ID);
            patcher.PatchAll();
            Debug.Log("[ProjectileReflector] Harmony patched");
            Compat.ModConfigMenu.Init();
        }
        void OnDisable()
        {
            patcher?.UnpatchAll(HARMONY_ID);
            Debug.Log("[ProjectileReflector] Harmony released");
            Compat.ModConfigMenu.Dispose();
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
    }

    public partial class ModConfigEntry
    {
        public const string CONFIG_FILE_NAME = ModBehaviour.HARMONY_ID + ".json";
        public static readonly string CONFIG_FILE_PATH = Path.Combine(SavesSystem.GetFullPathToSavesFolder(), CONFIG_FILE_NAME);
        public static ModConfigEntry INSTANCE { get => instance; }

        public static void Init()
        {
            InitWatcher();
            TryLoadConfig();
        }

        static FileSystemWatcher configWatcher;
        static bool skipNextChange = false;
        static void InitWatcher()
        {
            if (configWatcher != null) return;
            configWatcher = new FileSystemWatcher(SavesSystem.GetFullPathToSavesFolder());
            configWatcher.Filter = CONFIG_FILE_NAME;
            configWatcher.NotifyFilter = NotifyFilters.LastWrite;
            configWatcher.Changed += (sender, args) =>
            {
                if (skipNextChange)
                {
                    //Debug.Log("[ProjectileReflector] skip watcher trigger");
                    skipNextChange = false;
                    return;
                }
                Debug.Log("[ProjectileReflector] watcher trigger, do extra load");
                TryLoadConfig();
            };
            configWatcher.EnableRaisingEvents = true;
        }
        public static void TryLoadConfig()
        {
            if (File.Exists(CONFIG_FILE_PATH))
            {
                try
                {
                    var raw = File.ReadAllText(CONFIG_FILE_PATH, System.Text.Encoding.UTF8);
                    JsonUtility.FromJsonOverwrite(raw, instance);
                }
                catch (Exception e)
                {
                    Debug.LogError("[ProjectileReflector] error when loading config:");
                    Debug.LogException(e);
                }
            }
            else
            {
                Debug.Log("[ProjectileReflector] creating new config file to: " + CONFIG_FILE_PATH);
            }
            // always save for updated entries
            SaveConfig();
        }
        public static void SaveConfig()
        {
            skipNextChange = true;
            try
            {
                var output = JsonUtility.ToJson(instance, true);
                File.WriteAllText(CONFIG_FILE_PATH, output, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                Debug.LogError("[ProjectileReflector] error when saving config:");
                Debug.LogException(e);
                skipNextChange = false;
            }
        }
    }
}