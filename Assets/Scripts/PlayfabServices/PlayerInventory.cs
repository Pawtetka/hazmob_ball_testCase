using System;
using System.Collections.Generic;
using PlayFab.ClientModels;

namespace PlayfabServices
{
    [Serializable]
    public class PlayerInventory
    {
        public List<ItemInstance> Items;

        public PlayerInventory()
        {
            Items = new List<ItemInstance>();
        }
    }
}