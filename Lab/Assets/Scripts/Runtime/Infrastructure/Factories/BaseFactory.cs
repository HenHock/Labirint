using Runtime.Services.Providers.AssetsProvider;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Factories
{
    public abstract class BaseFactory
    {
        protected readonly DiContainer Container;
        protected readonly IAssetProvider AssetProvider;

        protected BaseFactory(DiContainer container, IAssetProvider assetProvider)
        {
            Container = container;
            AssetProvider = assetProvider;
        }

        protected TComponent InstantiateAsset<TComponent>(object key = null, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where TComponent : Component => 
            InstantiatePrefab(AssetProvider.Get<TComponent>(key), position, rotation, parent);

        protected GameObject InstantiateAsset(object key = null, Vector3 position = default, Quaternion rotation = default, Transform parent = null) => 
            InstantiatePrefab(AssetProvider.Get<GameObject>(key), position, rotation, parent);

        protected TComponent InstantiatePrefab<TComponent>(TComponent prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where TComponent : Component
        {
            var instance = Object.Instantiate(prefab, position, rotation, parent);
            Container.InjectGameObject(instance.gameObject);
            
            return instance;
        }

        protected GameObject InstantiatePrefab(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            var instance = Object.Instantiate(prefab, position, rotation, parent);
            Container.InjectGameObject(instance);
            
            return instance;
        }

        public virtual void Cleanup(){}
    }
}