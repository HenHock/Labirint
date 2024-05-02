using Runtime.Infrastructure.Bootstrap.ScenesStateMachine;
using Runtime.Infrastructure.Factories;
using Runtime.Services.PauseService;
using Runtime.Services.WindowService;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneStateMachine();
            BindUIFactory();
            BindGameFactory();
            BindPauseService();
            BindWindowService();
            
            Debug.Log("Bound gameplay services");
        }

        private void BindSceneStateMachine() => Container
            .BindInterfacesAndSelfTo<SceneStateMachine>()
            .AsSingle();

        private void BindGameFactory() => Container
            .BindInterfacesAndSelfTo<GameFactory>()
            .AsSingle();

        private void BindUIFactory() => Container
            .BindInterfacesAndSelfTo<UIFactory>()
            .AsSingle();

        private void BindPauseService() => Container
            .BindInterfacesAndSelfTo<PauseService>()
            .AsSingle();

        private void BindWindowService() => Container
            .BindInterfacesAndSelfTo<WindowService>()
            .AsSingle();
    }
}