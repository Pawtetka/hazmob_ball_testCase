using System;
using System.Collections.Generic;
using DefaultNamespace;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayfabServices
{
    public class PlayerDataManager : MonoBehaviour
    {
        private PlayerData _playerData;
        private PlayerInventory _playerInventory;
        public PlayerData PlayerData => _playerData;
        public PlayerInventory Inventory => _playerInventory;

        public Action OnPlayerDataUpdated;

        public void Start()
        {
            PlayerDataEventsBus.UpdateUserHighscore += UpdateHighscore;
            PlayerDataEventsBus.EquipNewSkin += UpdateChosenSkin;
        }

        public void Initialize(PlayerData data, PlayerInventory inventory)
        {
            _playerData = data;
            _playerInventory = inventory;
        }

        public void UpdateHighscore(int newHighscore)
        {
            if (newHighscore > _playerData.Highscore)
            {
                _playerData.Highscore = newHighscore;
                OnPlayerDataUpdated?.Invoke();
            }
        }

        public void UpdateChosenSkin(Skins newSkin)
        {
            if (!_playerData.ActualSkin.Equals(newSkin))
            {
                _playerData.ActualSkin = newSkin;
                OnPlayerDataUpdated?.Invoke();
            }
        }
        
        public void UpdateUserInventoryItems(List<ItemInstance> newInventory)
        {
            _playerInventory.Items = newInventory;
        }
        
        public void UpdateUserCoinsBalance(int newBalance)
        {
            _playerInventory.Coins = newBalance;
        }
    }
}