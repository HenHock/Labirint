using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.StateFactory;
using Runtime.Infrastructure.Factories;
using Runtime.Services.Input;
using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.Save;
using Runtime.Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputService();
            BingStateFactory();
            BingGameStateMachine();
            BindAssetProvider();
            BindConfigProvider();
            BindSceneLoader();
            BindSaveLoadService();
            BindPersistenceProgressService();

            Debug.Log("Bound game services");
        }

        private void BingStateFactory() => Container
                .BindInterfacesAndSelfTo<StateFactory>()
                .AsSingle()
                .CopyIntoAllSubContainers();

        private void BindInputService() => Container
            .BindInterfacesTo<StandaloneInputService>()
            .AsSingle()
            .CopyIntoAllSubContainers();

        private void BingGameStateMachine() => Container
            .BindInterfacesAndSelfTo<GameStateMachine>()
            .AsSingle()
            .CopyIntoAllSubContainers();

        private void BindAssetProvider() => Container
            .BindInterfacesTo<AssetProvider>()
            .AsSingle()
            .CopyIntoAllSubContainers();

        private void BindConfigProvider() => Container
            .BindInterfacesAndSelfTo<ConfigProvider>()
            .AsSingle()
            .CopyIntoAllSubContainers();

        private void BindSceneLoader() => Container
            .BindInterfacesTo<SceneLoader>()
            .AsSingle();
        
        private void BindSaveLoadService() => Container
            .BindInterfacesTo<SaveLoadService>()
            .AsSingle();

        private void BindPersistenceProgressService() => Container
            .BindInterfacesTo<PersistentProgressService>()
            .AsSingle();
    }
}