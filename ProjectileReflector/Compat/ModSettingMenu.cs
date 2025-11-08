using System;
using System.Linq;
using Duckov.Modding;
using SodaCraft.Localizations;
using UnityEngine;
using YukkuriC;

namespace ProjectileReflector.Compat
{
    public static partial class ModSettingMenu
    {
        private static ModInfo myInfo;
        public static void Init(ModInfo info)
        {
            myInfo = info;
            ModManager.OnModActivated += OnModActivated;
            ModManager.OnModWillBeDeactivated += OnModWillBeDeactivated;
            if (ModSettingAPI.Init(myInfo)) AddUI();
        }
        public static void Dispose()
        {
            ModManager.OnModActivated -= OnModActivated;
            ModManager.OnModWillBeDeactivated -= OnModWillBeDeactivated;
        }

        static void OnModActivated(ModInfo modInfo, Duckov.Modding.ModBehaviour behaviour)
        {
            if (modInfo.name != ModSettingAPI.MOD_NAME || !ModSettingAPI.Init(myInfo)) return;
            if (ModSettingAPI.Init(myInfo)) AddUI();
        }
        static void OnModWillBeDeactivated(ModInfo modInfo, Duckov.Modding.ModBehaviour behaviour)
        {
            if (modInfo.name != ModSettingAPI.MOD_NAME || !ModSettingAPI.Init(myInfo)) return;
            // TODO remove custom watcher
        }

        static void AddUI()
        {
            AddUI(CommonLib.IsChinese());
        }
        static Action<T> WrapOnChange<T>(Action<T> original)
        {
            return v =>
            {
                original(v);
                ModConfigEntry.SaveConfig();
            };
        }
    }
}
