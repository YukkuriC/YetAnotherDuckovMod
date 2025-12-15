using ItemStatsSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace YukkuriC.AlienGuns.Components
{
    public class ItemSetting_AmmoExtender : ItemSettingBase
    {
        public int targetSize = 100;
        public override void SetMarkerParam(Item selfItem)
        {
            selfItem.Inventory.SetCapacity(targetSize); // _(:з」∠)_
        }
    }
}
