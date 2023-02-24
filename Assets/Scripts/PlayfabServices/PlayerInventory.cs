using System;
using System.Collections.Generic;
using PlayFab.ClientModels;

namespace PlayfabServices
{
    [Serializable]
    public class PlayerInventory
    {
        public int Coins;
        public List<ItemInstance> Items;

        public PlayerInventory()
        {
            Coins = 0;
            Items = new List<ItemInstance>();
        }
    }
}