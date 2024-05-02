using System.Linq;
using Runtime.Configs;
using Runtime.Configs.Infrastructure;
using Runtime.Logic.UI;
using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.SaveSystem.SaveLoadService;
using Runtime.Services.WindowService;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Factories
{
    public sealed class UIFactory : BaseFactory
    {
        private readonly IConfigProvider _configProvider;
        
        private Transform _uiRoot;

        public UIFactory(DiContainer container, IAssetProvider assetProvider, IConfigProvider configProvider, ISaveLoadService saveLoadService) 
            : base(container, assetProvider, saveLoadService)
        {
            _configProvider = configProvider;
        }

        public override void Cleanup() => _uiRoot = null;

        public void CreateUIRoot() => 
            _uiRoot = Object.Instantiate(AssetProvider.Get<Transform>(AssetProviderKey.UIRoot));

        public void CreateGameplayHUD() => InstantiateUIAsset<GameplayHUDDisplay>(_uiRoot);

        public BaseWindow CreateWindow(WindowType windowType)
        {
            var windowConfig = GetWindowConfig(windowType);
            return InstantiateUIPrefab(windowConfig.m_WindowPrefab, _uiRoot);
        }

        private TComponent InstantiateUIAsset<TComponent>(Transform parent) where TComponent : Component
        {
            var instance = InstantiateAsset<TComponent>();
            instance.transform.SetParent(parent, false);
            instance.transform.localScale = Vector3.one;
            
            return instance;
        }

        private TComponent InstantiateUIPrefab<TComponent>(TComponent prefab, Transform parent) where TComponent : Component
        {
            var instance = InstantiatePrefab(prefab);
            instance.transform.SetParent(parent, false);
            instance.transform.localScale = Vector3.one;
            
            return instance;
        }

        private WindowConfig GetWindowConfig(WindowType windowType) => 
            _configProvider.Get<WindowConfig[]>().FirstOrDefault(config => config.m_WindowType == windowType);
    }
} 