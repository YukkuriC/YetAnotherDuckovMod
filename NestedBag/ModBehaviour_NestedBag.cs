using ItemStatsSystem;
using ItemStatsSystem.Items;

namespace NestedBag
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        const int BAG_ID = 1255;
        const int EXTEND_SLOTS_COUNT = 6;
        Slot? cachedOriginal;

        void OnEnable()
        {
            var bag = ItemAssetsCollection.GetPrefab(BAG_ID);
            var slots = bag.Slots;
            cachedOriginal = slots[0];
            slots.Clear();
            for (int i = 0; i < EXTEND_SLOTS_COUNT; i++)
            {
                slots.Add(new Slot($"bag_{i}"));
            }
        }
        void OnDisable()
        {
            if (cachedOriginal == null) return;
            var bag = ItemAssetsCollection.GetPrefab(BAG_ID);
            var slots = bag.Slots;
            slots.Clear();
            slots.Add(cachedOriginal);
        }
    }
}
