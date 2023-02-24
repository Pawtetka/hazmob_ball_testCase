using System;
using System.Collections.Generic;
using PlayfabServices;

namespace DefaultNamespace
{
    public static class PlayfabEventsBus
    {
        public static Action OnLoginSuccess;
        public static Action OnFirstLoginDetected;
        public static Action OnShopUpdated;
        public static Action OnCoinsUpdated;
    }
}