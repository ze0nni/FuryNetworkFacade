using System.Threading.Tasks;
using UnityEngine;

namespace Fury.DebugNetwork
{
    public class DebugNetwork : INetwork
    {
        NetworkController _controller;
        NetworkUser _user;
        DebugNetworkPrefs _prefs;

        public Task<NetworkUser> Init(NetworkController controller)
        {
            _controller = controller;
            _prefs = DebugNetworkPrefs.Instance;
            _user = _controller.NewUser(_prefs.UserId);
            _user.UpdateNickname(_prefs.UserNickname);
            _user.UpdatePhoto(_prefs.UserPhoto);
            return Task.FromResult(_user);
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
