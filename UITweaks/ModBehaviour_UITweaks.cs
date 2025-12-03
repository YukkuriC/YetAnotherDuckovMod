using HarmonyLib;
using ItemStatsSystem;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace YukkuriC.UITweaks
{
    [HarmonyPatch]
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        public const string HARMONY_ID = "YukkuriC.UITweaks";
        public static readonly string ROOT_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        Harmony? patcher;
        void OnEnable()
        {
            if (patcher == null) patcher = new Harmony(HARMONY_ID);
            patcher.PatchAll();
            Debug.Log("[UITweaks] Harmony patched");

            // 暗改！
            ItemAssetsCollection.GetPrefab(397).Inventory.SetCapacity(15);
            ItemAssetsCollection.GetPrefab(388).MaxStackCount = 100;
        }
        void OnDisable()
        {
            patcher?.UnpatchAll(HARMONY_ID);
            Debug.Log("[UITweaks] Harmony released");
        }
    }
}