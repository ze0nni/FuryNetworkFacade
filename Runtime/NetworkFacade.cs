using System.Threading.Tasks;
using UnityEngine;

namespace Fury {
    public static class NetworkFacade
    {
        static readonly INetwork _network;

        static NetworkFacade()
        {
            var go = new GameObject(typeof(NetworkFacade).FullName);
            GameObject.DontDestroyOnLoad(go);

            _network =
#if YA_GAMES
                go.AddComponent<Fury.YaGamesNetwork.YaGamesNetwork>();
#else
                new Fury.DebugNetwork.DebugNetwork();
#endif
        }

        public static Task Init()
        {
            return _network.Init();
        }

        public static IStorage<T> Storage<T>(string key) where T : class
        {
            return new Storage<T>(_network, $"{key}%{typeof(T).FullName}");
        }

        public static bool HasRewardVideo() => _network.HasRewardVideo;

        public static Task<bool> ShowRewardVideo() => _network.ShowRewardVideo();
    }
}