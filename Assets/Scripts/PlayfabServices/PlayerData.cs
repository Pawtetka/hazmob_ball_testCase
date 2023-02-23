using System;
using System.Collections.Generic;

namespace PlayfabServices
{
    [Serializable]
    public class PlayerData
    {
        public int Coins;
        public int Highscore;
        public Skins ActualSkin;
        public PlayerInventory PlayerInventory;

        public PlayerData()
        {
            Coins = 0;
            Highscore = 0;
            ActualSkin = Skins.Default;
            PlayerInventory = new PlayerInventory();
        }
    }
}