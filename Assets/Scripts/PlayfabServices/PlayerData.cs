using System;
using System.Collections.Generic;

namespace PlayfabServices
{
    [Serializable]
    public class PlayerData
    {
        public int Highscore;
        public Skins ActualSkin;

        public PlayerData()
        {
            Highscore = 0;
            ActualSkin = Skins.Default;
        }
    }
}