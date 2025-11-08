using System.Linq;
using SodaCraft.Localizations;
using UnityEngine;

namespace YukkuriC
{
    public static class CommonLib
    {
        public static bool IsChinese()
        {
            return LocalizationManager.CurrentLanguage.ToString().StartsWith("Chinese");
        }
    }
}