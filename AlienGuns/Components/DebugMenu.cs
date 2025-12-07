using Duckov.Scenes;
using ItemStatsSystem;
using UnityEngine;
using YukkuriC.AlienGuns.Items;

namespace YukkuriC.AlienGuns.Components
{
    public class DebugMenu : MonoBehaviour
    {
        bool debugGUIShow = false;
        Rect windowRect = new Rect(200, 200, 300, 500);
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                debugGUIShow = !debugGUIShow;
                var player = CharacterMainControl.Main;
                player.PopText($"{MultiSceneCore.Instance.SceneInfo.ID} {player.transform.position}");
            }
        }
        void OnGUI()
        {
            if (!debugGUIShow) return;
            var player = LevelManager.Instance?.MainCharacter;
            GUILayout.BeginArea(windowRect);
            foreach (var prefab in GunRegistry.AddedGuns)
            {
                if (GUILayout.Button($"#{prefab.TypeID}: {prefab.DisplayName}"))
                {
                    ItemUtilities.SendToPlayer(ItemAssetsCollection.InstantiateSync(prefab.TypeID));
                }
            }
            GUILayout.EndArea();
        }
    }
}
