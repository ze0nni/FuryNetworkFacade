using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

namespace Fury
{
    public interface IStorage<T> where T : class
    {
        Task<T> Load();
        void Save(T data);
        void Delete();
    }

    class Storage<T> : IStorage<T> where T : class
    {
        readonly INetwork _network;
        readonly string _key;
        readonly MemoryStream _ms = new MemoryStream();

        public Storage(INetwork network, string key)
        {
            _network = network;
            _key = key;
        }

        public async Task<T> Load()
        {
            try
            {
                var str = await _network.LoadData(_key);
                if (String.IsNullOrEmpty(str))
                {
                    return null;
                }
                var bytes = Convert.FromBase64String(str);
                if (bytes.Length == 0)
                {
                    return null;
                }

                _ms.SetLength(0);
                _ms.Write(bytes);
                _ms.Position = 0;
                var bf = new BinaryFormatter();
                return (T)bf.Deserialize(_ms);
            }
            catch (Exception exc)
            {
                Debug.LogException(exc);
                return null;
            }
        }

        public void Save(T data)
        {
            _ms.SetLength(0);
            var bf = new BinaryFormatter();
            bf.Serialize(_ms, data);
            var bytes = _ms.ToArray();
            var str = Convert.ToBase64String(bytes);
            _network.SaveData(_key, str);
        }

        public void Delete()
        {
            _network.DeleteData(_key);
        }
    }
}