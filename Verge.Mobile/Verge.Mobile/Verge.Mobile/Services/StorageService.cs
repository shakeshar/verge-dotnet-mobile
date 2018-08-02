using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Verge.Mobile.Services
{
    public class StorageService : IStorageService
    {
        public IDictionary<string, object> Cache { get; }
        public string Key { get; private set; }
        public StorageService()
        {
            Cache = ViewModelLocator.Resolve<IDictionary<string, object>>();
        }
        public T GetItem<T>(string key)
            where T : new()
        {
            Key = key;
            if (Cache.ContainsKey(key))
            {
                if (Cache[key] is T) return (T)Cache[key];                
                Cache[key] = ((JToken)Cache[Key]).ToObject<T>();
                return (T)Cache[key];
            }
            else
            {
                Cache.Add(key, new T());
                return (T)Cache[key];
            }
        }
    }
}
