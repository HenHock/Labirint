using Runtime.Infrastructure.Bootstrap.GameStateMachine;
using Runtime.Infrastructure.Bootstrap.GameStateMachine.StateFactory;
using Runtime.Infrastructure.Factories;
using Runtime.Services.Input;
using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.Save;
using Runtime.Services.SceneLoader;
using Zenject;

namespace Runtime.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputService();
            BingStateFactory();
            BingGameStateMachine();
            BindAssetProvider();
            BindConfigProvider();
            BindSceneLoader();
            BindGameFactory();
            BindUIFactory();
            BindSaveLoadService();
            BindPersistenceProgressService();
        }

        private void BingStateFactory() => Container
                .BindInterfacesAndSelfTo<StateFactory>()
                .AsSingle();

        private void BindInputService() => Container
            .BindInterfacesTo<StandaloneInputService>()
            .AsSingle();

        private void BingGameStateMachine() => Container
            .BindInterfacesAndSelfTo<GameStateMachine>()
            .AsSingle();

        private void BindAssetProvider() => Container
            .BindInterfacesTo<AssetProvider>()
            .AsSingle();

        private void BindConfigProvider() => Container
            .BindInterfacesAndSelfTo<ConfigProvider>()
            .AsSingle();

        private void BindSceneLoader() => Container
            .BindInterfacesTo<SceneLoader>()
            .AsSingle();

        private void BindGameFactory() => Container
            .BindInterfacesAndSelfTo<GameFactory>()
            .AsSingle();

        private void BindUIFactory() => Container
            .BindInterfacesAndSelfTo<UIFactory>()
            .AsSingle();

        private void BindSaveLoadService() => Container
            .BindInterfacesTo<SaveLoadService>()
            .AsSingle();

        private void BindPersistenceProgressService() => Container
            .BindInterfacesTo<PersistentProgressService>()
            .AsSingle();
    }
}