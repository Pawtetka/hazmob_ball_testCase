using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using PlayFab.ClientModels;
using PlayfabServices;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField] private LoginService loginService;
    [SerializeField] private InventoryService inventoryService;

    private PlayerData _playerData;
    public PlayerData PlayerData => _playerData;

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
        PlayfabEventsBus.OnUserDataUpdated += UpdateUserData;
    }
    
    public void LoginUser()
    {
        loginService.Login();
    }

    private void UpdateUserData()
    {
        inventoryService.GetUserData(loginService.UserId, SetNewPlayerData);
    }

    private void SetNewPlayerData(PlayerData data)
    {
        _playerData = data;
    }

}
