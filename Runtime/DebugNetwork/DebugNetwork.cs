using System.Threading.Tasks;
using UnityEngine;

namespace Fury.DebugNetwork
{
    public class DebugNetwork : INetwork
    {
        public Task Init()
        {
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

        public void DeleteData(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
