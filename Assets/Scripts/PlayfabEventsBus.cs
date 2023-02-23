using System;
using PlayfabServices;

namespace DefaultNamespace
{
    public static class PlayfabEventsBus
    {
        public static Action OnLoginSuccess;
        public static Action OnFirstLoginDetected;
        public static Action OnUserDataUpdated;
        public static Action OnUserCoinsUpdated;
    }
}