using System;
using PlayfabServices;

namespace DefaultNamespace
{
    public static class PlayerDataEventsBus
    {
        public static Action<int> UpdateUserHighscore;
        public static Action<Skins> EquipNewSkin;
        public static Action<int> BuyItem;
        public static Action<int> AddCoins;
    }
}