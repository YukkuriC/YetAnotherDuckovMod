using ItemStatsSystem;
using SodaCraft.Localizations;
using UnityEngine;

namespace CashAsAmmo.Components
{
    using static ModBehaviour;
    public class SwapAmmoTypeAction : UsageBehavior
    {
        public override bool CanBeUsed(Item item, object user) => true;

        DisplaySettingsData _cachedDisplay = new DisplaySettingsData() { display = true };
        public override DisplaySettingsData DisplaySettings
        {
            get
            {
                _cachedDisplay.description = LocalizationManager.GetPlainText(LANG_KEY_TOOLTIP);
                return _cachedDisplay;
            }
        }

        protected override void OnUse(Item master, object user)
        {
            if (!(user is CharacterMainControl player)) return;

            Item gun = lastHoldingWeapon;
            Debug.Log($"last holding: {gun?.DisplayName}");
            if (gun == null)
            {
                gun = player.PrimWeaponSlot().Content;
                if (gun == null) gun = player.SecWeaponSlot().Content;
            }
            var caliber = gun?.Constants.GetString(HASH_CALIBER) ?? "Item_Cash";

            player.PopText(LocalizationManager.GetPlainText(LANG_KEY_POPUP) + ": " + LocalizationManager.GetPlainText(caliber));
            master.Constants.SetString(HASH_CALIBER, caliber);
            master.StackCount++; // counters CA_UseItem.OnFinish
        }

        static Item lastHoldingWeapon;
        public static void OnAfterLevelInit()
        {
            lastHoldingWeapon = null;
            var player = CharacterMainControl.Main;
            if (player == null) return;
            player.agentHolder.OnHoldAgentChanged += agent =>
            {
                if (agent is ItemAgent_Gun) lastHoldingWeapon = agent.Item;
            };
        }
    }
}
