using System.Threading.Tasks;
using UnityEngine;

namespace Fury {
    public static class NetworkFacade
    {
        static readonly NetworkController _controller;
        internal static NetworkFacadeComponent Component { get; private set; }
        static INetwork _network;

        static NetworkFacade()
        {
            var go = new GameObject(typeof(NetworkFacade).FullName);
            GameObject.DontDestroyOnLoad(go);

            _controller = new NetworkController();
            var Component = go.AddComponent<NetworkFacadeComponent>();
            Component.Controller = _controller;

            _network =
#if YA_GAMES
                go.AddComponent<Fury.YaGamesNetwork.YaGamesNetwork>();
#else
                new Fury.DebugNetwork.DebugNetwork();
#endif
        }

        public static async Task Init()
        {
            User = await _network.Init(_controller);
        }

        public static NetworkUser User { get; private set; }

        public static IStorage<T> Storage<T>(string key) where T : class
        {
            return new Storage<T>(_network, $"{key}%{typeof(T).FullName}");
        }

        public static Task<bool> ShowFullscreenVideo() => _network.ShowFullscreenVideo();

        public static bool HasRewardVideo() => _network.HasRewardVideo;

        public static Task<bool> ShowRewardVideo() => _network.ShowRewardVideo();
    }
}