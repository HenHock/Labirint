using System.Collections.Generic;
using Runtime.Configs;
using UnityEngine;

namespace Runtime.Services.Providers
{
    public abstract class Provider : IProvider
    {
        private readonly IDictionary<string, object> _provideObjects = new Dictionary<string, object>();

        public TData Get<TData>(object key = null)
        {
            var cKey = GetKey<TData>(key);
            if (_provideObjects.TryGetValue(cKey, out var value))
                return (TData)value;

            Debug.LogError($"{GetType().Name}: Doesn't contain the {cKey} key.");
            return default;
        }

        public TData[] LoadAll<TData>(string folderPath) where TData : Object => 
            Resources.LoadAll<TData>(folderPath);

        public TData Load<TData>(string dataPath) where TData : Object
        {
            var loadData = Resources.Load<TData>(dataPath);
            Debug.Assert(loadData != null, $"{GetType().Name}: Couldn't load the ({typeof(TData).Name} data by path: {dataPath})");
            
            return loadData;
        }

        protected void Add<TData>(object key, TData value)
        {
            var cKey = typeof(TData).Name + key;
            if (value != null)
            {
                if (!_provideObjects.ContainsKey(cKey))
                    _provideObjects.Add(cKey, value);
            }
            else Debug.LogError($"{GetType().Name}: Couldn't find the object with key({cKey})");
        }

        protected void Add<TData>(TData value)
        {
            if (value != null)
            {
                var cKey = typeof(TData).Name;
                if (!_provideObjects.ContainsKey(cKey))
                    _provideObjects.Add(cKey, value);
            }
        }

        protected void Remove<TData>(object key = null)
        {
            var cKey = GetKey<TData>(key);
            if (_provideObjects.ContainsKey(cKey)) 
                _provideObjects.Remove(cKey);
            else Debug.LogError($"{GetType().Name}: Couldn't find the object with key({cKey}) to remove it");
        }

        private string GetKey<TData>(object key) => 
            key == null ? typeof(TData).Name : typeof(TData).Name + key;
    }
}