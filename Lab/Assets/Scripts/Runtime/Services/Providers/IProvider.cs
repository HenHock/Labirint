using UnityEngine;

namespace Runtime.Services.Providers
{
    public interface IProvider
    {
        TData Get<TData>(object key = null);
        TData Load<TData>(string dataPath) where TData : Object;
        TData[] LoadAll<TData>(string folderPath) where TData : Object;
    }
}