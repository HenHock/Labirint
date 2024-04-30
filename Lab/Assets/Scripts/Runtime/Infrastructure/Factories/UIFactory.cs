using Runtime.Configs;
using Runtime.Configs.Infrastructure;
using Runtime.Services.Providers;
using Runtime.Services.Providers.AssetsProvider;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Factories
{
    public sealed class UIFactory : BaseFactory
    {
        private Transform _uiRoot;

        public UIFactory(DiContainer container, IAssetProvider assetProvider) : base(container, assetProvider)
        {}

        public Transform CreateUIRoot() => 
            _uiRoot = Object.Instantiate(AssetProvider.Get<Transform>(AssetProviderKey.UIRoot));

        public Transform CreateMainMenu(Transform parent) => 
            InstantiateUIAsset<Transform>(parent);

        private TComponent InstantiateUIAsset<TComponent>(Transform parent) where TComponent : Component
        {
            var instance = InstantiateAsset<TComponent>();
            instance.transform.SetParent(parent, false);
            instance.transform.localScale = Vector3.one;
            
            return instance;
        }
    }
} 