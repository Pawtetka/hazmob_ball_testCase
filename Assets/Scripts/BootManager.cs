using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BootManager : MonoBehaviour
    {
        public void Start()
        {
            PlayfabEventsBus.OnLoginSuccess += LoadMainMenu;
        }

        private void LoadMainMenu()
        {
            ScenesLoader.Instance.LoadScene(Scenes.MainMenu);
        }
    }
}