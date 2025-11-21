using ItemStatsSystem;
using SodaCraft.Localizations;

namespace CashAsAmmo.Components
{
    using static ModBehaviour;
    public class SwapAmmoTypeAction : UsageBehavior
    {
        public override bool CanBeUsed(Item item, object user) => true;

        int curIndex = 0;

        DisplaySettingsData _cachedDisplay = new DisplaySettingsData() { display = true };
        public override DisplaySettingsData DisplaySettings
        {
            get
            {
                _cachedDisplay.description = $"{LocalizationManager.GetPlainText(LANG_KEY_POPUP)}: {LocalizationManager.GetPlainText(NextType)}";
                return _cachedDisplay;
            }
        }
        int NextIdx => (curIndex + 1) % ALL_BULLET_TYPES.Length;
        string NextType => ALL_BULLET_TYPES[NextIdx];

        protected override void OnUse(Item master, object user)
        {
            var player = LevelManager.Instance?.MainCharacter;
            if (player != null) player.PopText(DisplaySettings.description);
            master.Constants.SetString(HASH_CALIBER, NextType);
            curIndex = NextIdx;
            master.StackCount++; // counters CA_UseItem.OnFinish
        }
    }
}
