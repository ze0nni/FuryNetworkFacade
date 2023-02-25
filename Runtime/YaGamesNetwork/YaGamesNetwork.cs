using AOT;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Fury.YaGamesNetwork
{
    public class YaGamesNetwork : MonoBehaviour, INetwork
    {
        #region Init
        TaskCompletionSource<bool> _initTaskSrc;
        public Task Init()
        {
            _initTaskSrc = new TaskCompletionSource<bool>();
            YASDK.YaGamesInit();
            return _initTaskSrc.Task;
        }

        public void OnInitCallbackOk()
        {
            _initTaskSrc.SetResult(true);
            _initTaskSrc = null;
            Debug.Log($"{nameof(YASDK.YaGamesInit)}: ok");
        }

        public void OnInitCallbackError(string error)
        {
            _initTaskSrc.SetException(new Exception(error));
            _initTaskSrc = null;
            Debug.LogError($"{nameof(YASDK.YaGamesInit)}: {error}");
        }
        #endregion

        #region Storage

        TaskCompletionSource<StorageCache> _getGetStorageCacheTaskSrc;

        Task<StorageCache> GetStorageCache()
        {
            if (_getGetStorageCacheTaskSrc == null)
            {
                _getGetStorageCacheTaskSrc = new TaskCompletionSource<StorageCache>();
                YASDK.YaGamesGetData();
            }
            return _getGetStorageCacheTaskSrc.Task;
        }

        public void OnGetDataCallbackOk(string data)
        {
            var cache = new StorageCache(data);
            _getGetStorageCacheTaskSrc.SetResult(cache);
            Debug.Log($"{nameof(YASDK.YaGamesGetData)}: {cache}");
        }

        public void OnGetDataCallbackError(string error)
        {
            _getGetStorageCacheTaskSrc.SetException(new Exception(error));
            Debug.LogError($"{nameof(YASDK.YaGamesGetData)}: {error}");
        }

        public async Task<string> LoadData(string key)
        {
            var cache = await GetStorageCache();
            return cache.Get(key);
        }

        public async void SaveData(string key, string data)
        {
            var cache = await GetStorageCache();
            cache.Set(key, data);
            YASDK.YaGamesSetData(cache.ToJson());
        }

        public async void DeleteData(string key)
        {
            var cache = await GetStorageCache();
            cache.Delete(key);
            YASDK.YaGamesSetData(cache.ToJson());
        }

        #endregion
    }
}
