using System;
using System.Collections.Generic;
using DefaultNamespace;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EconomyModels;
using UnityEngine;

namespace PlayfabServices
{
    public class InventoryService : MonoBehaviour
    {
        public void Start()
        {
            PlayfabEventsBus.OnFirstLoginDetected += SetNewPlayerData;
        }

        public void GetUserData(string userId, Action<PlayerData> onSuccess)
        {
            var request = new GetUserDataRequest
            {
                PlayFabId = userId,
                Keys = null
            };
            PlayFabClientAPI.GetUserData(request, (result) =>
            {
                var data = OnGetResult(result); 
                GetInventoryData(userId, data, (playerData) => onSuccess?.Invoke(playerData));
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

        private void GetInventoryData(string userId, PlayerData data, Action<PlayerData> onSuccess)
        {
            var request = new GetUserInventoryRequest();
            PlayFabClientAPI.GetUserInventory(request, (result) =>
            {
                data.PlayerInventory.Items = result.Inventory; 
                onSuccess?.Invoke(data);
            }, OnError);
        }

        private PlayerData OnGetResult(GetUserDataResult result)
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
    }
}