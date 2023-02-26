using Newtonsoft.Json;
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
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception exc)
            {
                Debug.LogException(exc);
                return null;
            }
        }

        public void Save(T data)
        {
            _network.SaveData(_key, JsonConvert.SerializeObject(data));
        }

        public void Delete()
        {
            _network.DeleteData(_key);
        }
    }
}