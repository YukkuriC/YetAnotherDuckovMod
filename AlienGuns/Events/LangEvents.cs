using SodaCraft.Localizations;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC.AlienGuns.Ext;

namespace YukkuriC.AlienGuns.Events
{
    public static class LangEvents
    {
        public static void OnEnable()
        {
            LocalizationManager.OnSetLanguage += UpdateLang;
            UpdateLang(LocalizationManager.CurrentLanguage);
            // general
            foreach (var pair in $"lang.General.json".ToResourceJson<Dictionary<string, string>>()) LocalizationManager.SetOverrideText(pair.Key, pair.Value);
        }
        public static void OnDisable()
        {
            LocalizationManager.OnSetLanguage -= UpdateLang;
        }

        static void UpdateLang(SystemLanguage newLang)
        {
            var langDict = $"lang.{newLang}.json".ToResourceJson<Dictionary<string, string>>();
            if (langDict == null) return;
            foreach (var pair in langDict) LocalizationManager.SetOverrideText(pair.Key, pair.Value);
        }
    }
}
