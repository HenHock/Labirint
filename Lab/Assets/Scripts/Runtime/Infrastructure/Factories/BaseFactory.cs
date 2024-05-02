using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.SaveSystem;
using Runtime.Services.SaveSystem.SaveLoadService;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Factories
{
    public abstract class BaseFactory
    {
        protected readonly DiContainer Container;
        protected readonly IAssetProvider AssetProvider;
        protected readonly ISaveLoadService SaveLoadService;

        protected BaseFactory(DiContainer container, IAssetProvider assetProvider, ISaveLoadService saveLoadService)
        {
            Container = container;
            AssetProvider = assetProvider;
            SaveLoadService = saveLoadService;
        }

        public virtual void Cleanup(){}

        protected TComponent InstantiateAsset<TComponent>(object key = null, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where TComponent : Component => 
            InstantiatePrefab(AssetProvider.Get<TComponent>(key), position, rotation, parent);

        protected GameObject InstantiateAsset(object key = null, Vector3 position = default, Quaternion rotation = default, Transform parent = null) => 
            InstantiatePrefab(AssetProvider.Get<GameObject>(key), position, rotation, parent);

        protected TComponent InstantiatePrefab<TComponent>(TComponent prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where TComponent : Component
        {
            var instance = Object.Instantiate(prefab, position, rotation, parent);
            Container.InjectGameObject(instance.gameObject);
            RegisterInSaveSystem(instance.gameObject);
            
            return instance;
        }

        protected GameObject InstantiatePrefab(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            var instance = Object.Instantiate(prefab, position, rotation, parent);
            Container.InjectGameObject(instance);
            RegisterInSaveSystem(instance);
            
            return instance;
        }

        protected void RegisterInSaveSystem(GameObject instance)
        {
            foreach (var progressReader in instance.GetComponentsInChildren<IProgressReader>())
            {
                if (progressReader is IProgressWriter progressWriter)
                    SaveLoadService.ProgressWriters.Add(progressWriter);
                
                SaveLoadService.ProgressReaders.Add(progressReader);
            }
        }
    }
}