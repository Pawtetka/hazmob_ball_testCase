using System;
using PlayFab.ClientModels;

namespace PlayfabServices
{
    [Serializable]
    public class ShopItem
    {
        public StoreItem Item;
        public bool IsSold;
    }
}