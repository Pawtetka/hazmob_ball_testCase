using System;
using System.Collections.Generic;
using DefaultNamespace;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EconomyModels;
using Unity.VisualScripting;
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
            void OnSuccess(GetUserDataResult result)
            {
                var data = OnGetResult(result); 
                onSuccess?.Invoke(data);
            }
            
            var request = new GetUserDataRequest
            {
                PlayFabId = userId,
                Keys = null
            };
            PlayFabClientAPI.GetUserData(request, OnSuccess, OnError);
        }

        public void UpdatePlayerData(PlayerData data)
        {
            void OnUpdateSuccess(UpdateUserDataResult result)
            {
            }
            
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>()
                {
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

        public void GetInventoryData(Action<PlayerInventory> onSuccess = null)
        {
            void OnSuccess(GetUserInventoryResult result)
            {
                var playerInventory = new PlayerInventory
                {
                    Coins = result.VirtualCurrency[Enum.GetName(typeof(Currencies), Currencies.GC) ?? string.Empty],
                    Items = result.Inventory
                };
                onSuccess?.Invoke(playerInventory);
            }
            
            var request = new GetUserInventoryRequest();
            PlayFabClientAPI.GetUserInventory(request, OnSuccess, OnError);
        }

        public void AddCurrency(int value, string currencyId, Action<int> onSuccess = null)
        {
            void OnSuccess(ModifyUserVirtualCurrencyResult result)
            {
                onSuccess?.Invoke(result.Balance);
            }
            
            var request = new AddUserVirtualCurrencyRequest
            {
                Amount = value,
                VirtualCurrency = currencyId
            };
            PlayFabClientAPI.AddUserVirtualCurrency(request, OnSuccess, OnError);
        }
        
        public void SubtractCurrency(int value, string currencyId, Action<int> onSuccess = null)
        {
            void OnSuccess(ModifyUserVirtualCurrencyResult result)
            {
                onSuccess?.Invoke(result.Balance);
            }
            
            var request = new SubtractUserVirtualCurrencyRequest
            {
                Amount = value,
                VirtualCurrency = currencyId
            };
            PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSuccess, OnError);
        }

        private PlayerData OnGetResult(GetUserDataResult result)
        {
            if(result.Data == null || !result.Data.ContainsKey("Highscore") ||
               !result.Data.ContainsKey("ActualSkin")) return null;

            PlayerData data = new PlayerData
            {
                Highscore = int.Parse(result.Data["Highscore"].Value),
                ActualSkin = (Skins)int.Parse(result.Data["ActualSkin"].Value),
            };
            return data;
        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError("Inventory request failed - " + error.ErrorMessage);
        }
    }
}