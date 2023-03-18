using System.Runtime.InteropServices;

namespace Fury.YaGamesNetwork
{
    public static class YASDK
    {
        [DllImport("__Internal")]
        public static extern void YaGamesInit();

        [DllImport("__Internal")]
        public static extern void YaGamesGetData();

        [DllImport("__Internal")]
        public static extern void YaGamesSetData(string json);

        [DllImport("__Internal")]
        public static extern void YaGamesShowFullscreenAd();

        [DllImport("__Internal")]
        public static extern void YaGamesShowRewardAd();
    }
}
