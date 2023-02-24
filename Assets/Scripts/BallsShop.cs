using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayfabServices;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class BallsShop : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private GameObject shopItemPrefab;
        [SerializeField] private Button closeBtn;

        private List<ShopItemView> itemViews = new List<ShopItemView>();
        private PlayFabManager _playFabManager = PlayFabManager.Instance;
        List<ShopItem> shopItems = PlayFabManager.Instance.GetShopData();
        List<CatalogItem> catalog = PlayFabManager.Instance.GetCatalog();

        public void OnEnable()
        {
            
        }

        public bool Initialize()
        {
            CreateItemViews();
            
        }

        private void CreateItemViews()
        {
            foreach (var item in shopItems)
            {
                var newElement = Instantiate(shopItemPrefab, Vector3.zero, Quaternion.identity, container.transform).GetComponent<ShopItemView>();
                var catalogElement = catalog.Find(c => c.ItemId.Equals(item.Item.ItemId));
                var elementName = catalogElement.DisplayName;
                var price = (int)item.Item.VirtualCurrencyPrices["GC"];
                var color = JsonConvert.DeserializeObject<CatalogItemCustoInfo>(catalogElement.CustomData)!.Color;
                //newElement.Initialize(elementName, price, Currencies.GC, color, item.IsSold);
                itemViews.Add(newElement);
            }
        }

        private void UpdateItemsList()
        {
            
        }
    }

    [Serializable]
    public class CatalogItemCustoInfo
    {
        [JsonProperty("Radius")] public int Radius;
        [JsonProperty("Weight")] public int Weight;
        [JsonProperty("Color")] public int Color;
    }
}
