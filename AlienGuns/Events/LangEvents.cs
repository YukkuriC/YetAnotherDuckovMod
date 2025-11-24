using Newtonsoft.Json;
using SodaCraft.Localizations;
using System.Collections.Generic;
using System.Text;
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
