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
            var harmonyPath = Path.Combine(ROOT_PATH, "0Harmony.dll");
            Assembly.LoadFrom(harmonyPath);
            ModAudio.InitAudio();
            ModConfigEntry.TryLoadConfig();
        }
        void LateUpdate()
        {
            ModAudio.ClearPlayedFlag();
        }
    }

    public partial class ModConfigEntry
    {
        public static readonly string CONFIG_FILE_PATH = Path.Combine(SavesSystem.GetFullPathToSavesFolder(), ModBehaviour.HARMONY_ID + ".json");
        public static ModConfigEntry INSTANCE { get => instance; }

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
            var output = JsonUtility.ToJson(instance, true);
            File.WriteAllText(CONFIG_FILE_PATH, output, System.Text.Encoding.UTF8);
        }
    }
}