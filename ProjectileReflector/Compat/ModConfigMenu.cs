using Duckov.Modding;
using SodaCraft.Localizations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YukkuriC;

namespace ProjectileReflector.Compat
{
    public static partial class ModConfigMenu
    {
        const string MOD_NAME = ModBehaviour.HARMONY_ID;
        public static void Init()
        {
            ModManager.OnModActivated += OnModActivated;

            // 立即检查一次，防止 ModConfig 已经加载但事件错过了
            if (ModConfigAPI.IsAvailable()) OnModActivated();
        }
        public static void Dispose()
        {
            ModManager.OnModActivated -= OnModActivated;
            ModConfigAPI.SafeRemoveOnOptionsChangedDelegate(OnModConfigOptionsChanged);
        }

        static void OnModActivated(ModInfo info, Duckov.Modding.ModBehaviour behaviour)
        {
            if (info.name == ModConfigAPI.ModConfigName) OnModActivated();
        }
        static void OnModActivated()
        {
            SetupModConfig();
            LoadConfigFromModConfig();
        }

        static void SetupModConfig()
        {
            if (!ModConfigAPI.IsAvailable()) return;

            // 添加配置变更监听
            ModConfigAPI.SafeAddOnOptionsChangedDelegate(OnModConfigOptionsChanged);

            // 根据当前语言设置描述文字
            SystemLanguage[] chineseLanguages = {
                SystemLanguage.Chinese,
                SystemLanguage.ChineseSimplified,
                SystemLanguage.ChineseTraditional
            };
            bool isChinese = chineseLanguages.Contains(LocalizationManager.CurrentLanguage);
            SetupModConfig(isChinese);
        }

        static void OnModConfigOptionsChanged(string key)
        {
            if (!key.StartsWith(MOD_NAME + "_"))
                return;

            // 使用新的 LoadConfig 方法读取配置
            LoadConfigFromModConfig();

            // 保存到本地配置文件
            ModConfigEntry.SaveConfig();
        }
    }
}
