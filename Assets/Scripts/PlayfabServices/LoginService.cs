using System;
using DefaultNamespace;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

namespace PlayfabServices
{
    public class LoginService : MonoBehaviour
    {
        private string userId = "default";
        public string UserId => userId;
        public void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnResult, OnError);
        }

        private void OnResult(LoginResult result)
        {
            userId = result.PlayFabId;
            if(result.NewlyCreated) PlayfabEventsBus.OnFirstLoginDetected?.Invoke();
            PlayfabEventsBus.OnLoginSuccess?.Invoke();
        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError("Login failed - " + error.ErrorMessage);
        }
    }
}