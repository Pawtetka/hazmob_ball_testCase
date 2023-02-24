using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayfabServices
{
    public class ShopService : MonoBehaviour
    {
        private List<CatalogItem> _catalog = new List<CatalogItem>();
        private List<ShopItem> _currentShop = new List<ShopItem>();
        public List<CatalogItem> Catalog => _catalog;
        public List<ShopItem> ShopItems => _currentShop;

        public void GetCatalog()
        {
            void OnSuccess(GetCatalogItemsResult result)
            {
                _catalog = result.Catalog;
            }
            
            var request = new GetCatalogItemsRequest()
            {
                CatalogVersion = "Main"
            };
            PlayFabClientAPI.GetCatalogItems(request, OnSuccess, OnError);
        }
        
        public void UpdateStoreData(Stores storeId, PlayerInventory inventory, Action<List<ShopItem>> onSuccess = null)
        {
            void OnSuccess(GetStoreItemsResult result)
            {
                CreateStore(result, inventory); 
                onSuccess?.Invoke(ShopItems);
            }
            
            var request = new GetStoreItemsRequest
            {
                CatalogVersion = "Main",
                StoreId = ((int)storeId).ToString()
            };
            PlayFabClientAPI.GetStoreItems(request, OnSuccess, OnError);
        }

        public void BuyItem(int itemId, int storeId, Action<List<ItemInstance>> onSuccess = null)
        {
            void OnSuccess(PurchaseItemResult result)
            {
                onSuccess?.Invoke(result.Items);
            }
            
            var request = new PurchaseItemRequest
            {
                CatalogVersion = "Main",
                StoreId = ((int)storeId).ToString(),
                ItemId = itemId.ToString(),
                VirtualCurrency = Enum.GetName(typeof(Currencies), Currencies.GC)
            };
            PlayFabClientAPI.PurchaseItem(request, OnSuccess, OnError);
        }
        
        private void CreateStore(GetStoreItemsResult result, PlayerInventory inventory)
        {
            if (result.Store == null) return;

            _currentShop.Clear();
            foreach (var storeItem in result.Store)
            {
                bool isSold = inventory.Items.Any((i) => i.ItemId.Equals(storeItem.ItemId));
                _currentShop.Add(new ShopItem{Item = storeItem, IsSold = isSold});
            }
        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError("Inventory request failed - " + error.ErrorMessage);
        }
        
    }
    
}