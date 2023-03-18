using AOT;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

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

        #region RewardVideo

        TaskCompletionSource<bool> _rewardVideoSource;
        bool _rewardOpened;
        bool _rewarded;
        string _rewardedError;

        public bool HasRewardVideo => true;

        public Task<bool> ShowRewardVideo()
        {
            Assert.IsNull(_rewardVideoSource, "Reward video not complete");

            _rewardVideoSource = new TaskCompletionSource<bool>();
            _rewardOpened = false;
            _rewarded = false;
            _rewardedError = null;

            YASDK.YaGamesShowRewardAd();

            return _rewardVideoSource.Task;
        }

        public void OnRewardOpen()
        {
            Debug.Log($"{nameof(YASDK.YaGamesShowRewardAd)}: open");

            _rewardOpened = true;
        }

        public void OnRewarded()
        {
            Debug.Log($"{nameof(YASDK.YaGamesShowRewardAd)}: rewarded");
            _rewarded = true;
        }

        public void OnRewardClose()
        {
            Debug.Log($"{nameof(YASDK.YaGamesShowRewardAd)}: close");

            var src = _rewardVideoSource;
            _rewardVideoSource = null;
            if (_rewardedError != null)
            {
                src.SetException(new Exception(_rewardedError));
            } else
            {
                src.SetResult(_rewarded);
            }
        }

        public void OnRewardError(string error)
        {
            Debug.LogError($"{nameof(YASDK.YaGamesShowRewardAd)}: {error}");

            if (_rewardOpened)
            {
                _rewardedError = error;
            } else
            {
                var src = _rewardVideoSource;
                _rewardVideoSource = null;
                src.TrySetException(new Exception(error));
            }
        }

        #endregion
    }
}
