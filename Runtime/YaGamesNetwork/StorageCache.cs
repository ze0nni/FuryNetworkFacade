using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fury.YaGamesNetwork
{
    class StorageCache
    {
        readonly Dictionary<string, string> _cache;

        public StorageCache(string json)
        {
            _cache = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public string Get(string key)
        {
            _cache.TryGetValue(key, out var value);
            return value;
        }

        public void Set(string key, string value)
        {
            _cache[key] = value;
        }

        public void Delete(string key)
        {
            _cache.Remove(key);
        }

        public string ToJson(){
            var json = JsonConvert.SerializeObject(_cache);
            return json;
        }

        public override string ToString()
        {
            return $"{nameof(StorageCache)}({string.Join(", ", _cache.Keys)})";
        }
    }
}