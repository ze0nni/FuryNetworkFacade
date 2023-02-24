using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

namespace Fury.DebugNetwork
{
    class Storage<T> : IStorage<T> where T : class
    {
        readonly string _key;
        readonly MemoryStream _ms = new MemoryStream();

        public Storage(string key)
        {
            _key = $"{key}%{typeof(T).FullName}";
        }

        public async Task<T> Load()
        {
            try
            {
                var str = PlayerPrefs.GetString(_key);
                var bytes = Convert.FromBase64String(str);
                _ms.SetLength(0);
                _ms.Write(bytes);
                _ms.Position = 0;
                var bf = new BinaryFormatter();
                return (T)bf.Deserialize(_ms);
            } catch
            {
                return default;
            }
        }

        public async void Save(T data)
        {
            _ms.SetLength(0);
            var bf = new BinaryFormatter();
            bf.Serialize(_ms, data);
            var bytes = _ms.ToArray();
            var str = Convert.ToBase64String(bytes);
            PlayerPrefs.SetString(_key, str);
        }

        public async void Delete()
        {
            PlayerPrefs.DeleteKey(_key);
        }
    }
}