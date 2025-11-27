using ItemStatsSystem;
using UnityEngine;

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
    }
}
