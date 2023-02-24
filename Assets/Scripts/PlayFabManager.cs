using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using PlayFab.ClientModels;
using PlayfabServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField] private PlayerDataManager playerDataManager;
    [SerializeField] private LoginService loginService;
    [SerializeField] private InventoryService inventoryService;
    [SerializeField] private ShopService shopService;
    public PlayerDataManager PlayerDataManager => playerDataManager;

    #region Singleton

    public static PlayFabManager Instance = null;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad (gameObject);
    }

    #endregion

    public void Start()
    {
        PlayfabEventsBus.OnLoginSuccess += CreateUserData;
        PlayerDataManager.OnPlayerDataUpdated += UpdateUserData;
        PlayerDataEventsBus.BuyItem += BuyShopItem;
        PlayerDataEventsBus.AddCoins += AddCoins;
    }
    
    public void LoginUser()
    {
        loginService.Login();
    }

    public List<ShopItem> GetShopData()
    {
        return shopService.ShopItems;
    }

    public List<CatalogItem> GetCatalog()
    {
        return shopService.Catalog;
    }

    public void BuyShopItem(int itemId)
    {
        void OnSuccess(List<ItemInstance> items)
        {
            playerDataManager.UpdateUserInventoryItems(items);
        }
        
        shopService.BuyItem(itemId, (int)Stores.BallsStore);
    }
    
    public void AddCoins(int value)
    {
        void OnSuccess(int newBalance)
        {
            playerDataManager.UpdateUserCoinsBalance(newBalance);
        }

        if (playerDataManager.Inventory.Coins + value < 0)
        {
            Debug.LogError("You doesnt have money for this operation");
            return;
        }

        if (value > 0) 
            inventoryService.AddCurrency(value, Enum.GetName(typeof(Currencies), Currencies.GC), OnSuccess);
        else
            inventoryService.SubtractCurrency(value, Enum.GetName(typeof(Currencies), Currencies.GC), OnSuccess);
    }

    private void CreateUserData()
    {
        PlayerData playerData = new PlayerData();
        PlayerInventory playerInventory = new PlayerInventory();
        inventoryService.GetUserData(loginService.UserId, (data) => playerData = data);
        inventoryService.GetInventoryData((inventory) => playerInventory = inventory);
        playerDataManager.Initialize(playerData, playerInventory);
        CreateShop();
    }
    
    private void UpdateUserData()
    {
        inventoryService.UpdatePlayerData(playerDataManager.PlayerData);
    }

    private void CreateShop()
    {
        shopService.GetCatalog();
        shopService.UpdateStoreData((int)Stores.BallsStore, playerDataManager.Inventory);
    }

}
