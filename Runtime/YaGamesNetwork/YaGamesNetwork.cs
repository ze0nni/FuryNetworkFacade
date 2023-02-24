using AOT;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Fury.YaGamesNetwork
{
    public class YaGamesNetwork : MonoBehaviour, INetwork
    {
        [DllImport("__Internal")]
        private static extern void YaGamesInit();

        #region Init
        static TaskCompletionSource<bool> _initTaskSrc;
        public Task Init()
        {
            _initTaskSrc = new TaskCompletionSource<bool>();
            YaGamesInit();
            return _initTaskSrc.Task;
        }

        public void InitCallbackError(string error)
        {
            _initTaskSrc.SetException(new Exception(error));
            _initTaskSrc = null;
            Debug.LogError($"{nameof(YaGamesInit)}: {error}");
        }

        public void InitCallbackOk() {
            _initTaskSrc.SetResult(true);
            _initTaskSrc = null;
            Debug.Log($"{nameof(YaGamesInit)}: ok");
        }
        #endregion

        public IStorage<T> Storage<T>(string key) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}
