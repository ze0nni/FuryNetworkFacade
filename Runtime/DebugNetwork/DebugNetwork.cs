using System.Threading.Tasks;
using UnityEngine;

namespace Fury.DebugNetwork
{
    public class DebugNetwork : INetwork
    {
        DebugNetworkPrefs _prefs;

        public Task Init()
        {
            _prefs = DebugNetworkPrefs.Instance;
            return Task.CompletedTask;
        }

        public async Task<string> LoadData(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public void SaveData(string key, string data)
        {
            PlayerPrefs.SetString(key, data);
        }

        public Task<bool> ShowFullscreenVideo()
        {
            return Task.FromResult(_prefs.ShowFullscreenVideoSuccess);
        }

        public void DeleteData(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public bool HasRewardVideo => _prefs.HasRewardVideo;

        public Task<bool> ShowRewardVideo()
        {
            return Task.FromResult(_prefs.RewardVideoSuccessed);
        }
    }
}
