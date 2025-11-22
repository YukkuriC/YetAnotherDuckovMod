using System.IO;
using System.Reflection;
using UnityEngine;
using YukkuriC.AlienGuns.Items;

namespace YukkuriC.AlienGuns
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        public const string HARMONY_ID = "YukkuriC.AlienGuns";
        public static readonly string ROOT_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        void Awake()
        {
            GunRegistry.Init();
        }

        void OnEnable()
        {
            Events.AlienGunFireEvents.OnEnable();
            LangEvents.OnEnable();
        }
        void OnDisable()
        {
            Events.AlienGunFireEvents.OnDisable();
            LangEvents.OnDisable();
        }

#if DEBUG_MENU
        bool debugGUIShow = false;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                debugGUIShow = !debugGUIShow;
            }
        }
        void OnGUI()
        {
            if (debugGUIShow) GunRegistry.OnDebugGUI();
        }
#endif
    }
}