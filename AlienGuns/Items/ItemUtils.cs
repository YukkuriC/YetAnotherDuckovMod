using ItemStatsSystem;
using System.Collections.Generic;
using UnityEngine;
using YukkuriC.AlienGuns.Interfaces;

namespace YukkuriC.AlienGuns.Items
{
    public static class ItemUtils
    {
        public static readonly int ITEM_START_ID = "YukkuriC".GetHashCode();

        public static Item CopyItem(int original, int offset, bool writeBack = true)
        {
            var template = ItemAssetsCollection.InstantiateSync(original);
            template.SetTypeID(ITEM_START_ID + offset);
            Object.DontDestroyOnLoad(template);
            if (writeBack) ItemAssetsCollection.AddDynamicEntry(template);
            return template;
        }

        public static ItemAgent GetAgent(this Item item, int idx = 0) => item.AgentUtilities.agents[idx].agentPrefab;
        public static ItemAgent CopyAgent(this Item item, int idx = 0)
        {
            var originalAgentHolder = item.AgentUtilities.agents[idx];
            var agent = originalAgentHolder.agentPrefab = Object.Instantiate(originalAgentHolder.agentPrefab);
            agent.gameObject.SetActive(false);
            Object.DontDestroyOnLoad(agent.gameObject);
            return agent;
        }

        public static T AddUseItem<T>(this Item item) where T : UsageBehavior
        {
            item.AddUsageUtilitiesComponent();
            var usage = item.gameObject.AddComponent<T>();
            item.usageUtilities.behaviors.Add(usage);
            if (usage is ISetMasterItem binder) binder.SetMaster(item);
            return usage;
        }

        public static void AddMaterialToGun(ItemAgent_Gun agent, string matName, int rendererIndex = 0, bool clearOriginal = false) => AddMaterialToGun(agent, ResourceGrabber.Get<Material>(matName), rendererIndex, clearOriginal);
        public static void AddMaterialToGun(ItemAgent_Gun agent, Material mat, int rendererIndex = 0, bool clearOriginal = false)
        {
            var renderer = agent.GetComponent<CharacterSubVisuals>().renderers[0];
            var tmpList = new List<Material>();
            if (!clearOriginal) renderer.GetMaterials(tmpList);
            tmpList.Add(mat);
            renderer.SetMaterials(tmpList);
        }
    }
}
