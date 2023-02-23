using System;
using System.Collections.Generic;
using DefaultNamespace;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayfabServices
{
    public class ShopService : MonoBehaviour
    {
        /*
        public void Start()
        {
        }

        public void GetStoreData(Stores storeId, Action<List<ShopItem>> onSuccess)
        {
            var request = new GetStoreItemsRequest
            {
                CatalogVersion = "Main",
                StoreId = ((int)storeId).ToString()
            };
            PlayFabClientAPI.GetStoreItems(request, (result) =>
            {
                var data = OnGetResult(result); 
                onSuccess?.Invoke(data);
            }, OnError);
        }

        public void UpdatePlayerData(PlayerData data)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>()
                {
                    { "Coins", data.Coins.ToString() },
                    { "Highscore", data.Highscore.ToString() },
                    { "ActualSkin", ((int)data.ActualSkin).ToString()}
                }
            };
            PlayFabClientAPI.UpdateUserData(request, OnUpdateSuccess, OnError);
        }
        
        private void SetNewPlayerData()
        {
            PlayerData data = new PlayerData();
            UpdatePlayerData(data);
        }

        private List<StoreItem> OnGetResult(GetStoreItemsResult result)
        {
            if(result.Data == null || !result.Data.ContainsKey("Coins") || 
               !result.Data.ContainsKey("Highscore") || !result.Data.ContainsKey("ActualSkin")) return null;

            PlayerData data = new PlayerData
            {
                Coins = int.Parse(result.Data["Coins"].Value),
                Highscore = int.Parse(result.Data["Highscore"].Value),
                ActualSkin = (Skins)int.Parse(result.Data["ActualSkin"].Value),
            };
            return data;
        }

        private void OnUpdateSuccess(UpdateUserDataResult result)
        {
            PlayfabEventsBus.OnUserDataUpdated?.Invoke();
        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError("Inventory request failed - " + error.ErrorMessage);
        }
        */
    }
    
}