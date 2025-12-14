using System.IO;
using System.Reflection;
using YukkuriC.AlienGuns.Components;
using YukkuriC.AlienGuns.Events;
using YukkuriC.AlienGuns.Items;
using YukkuriC.AlienGuns.Misc;

namespace YukkuriC.AlienGuns
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        public const string HARMONY_ID = "YukkuriC.AlienGuns";
        public static readonly string ROOT_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        void Awake()
        {
            GunRegistry.Init();
            FormulaRegistry.Init();
            if (!SteamManager.Initialized)
            {
                gameObject.AddComponent<DebugMenu>();
            }
        }

        void OnEnable()
        {
            Events.AlienGunEvents.OnEnable();
            LangEvents.OnEnable();
            FormulaRegistry.Load();
            CustomMerchant.OnEnable();
        }
        void OnDisable()
        {
            Events.AlienGunEvents.OnDisable();
            LangEvents.OnDisable();
            FormulaRegistry.Unload();
            CustomMerchant.OnDisable();
        }
    }
}