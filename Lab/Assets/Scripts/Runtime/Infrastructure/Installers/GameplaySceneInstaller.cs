using Runtime.Infrastructure.Bootstrap;
using Runtime.Infrastructure.Bootstrap.ScenesStateMachine;
using Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneStateMachine();
            BindUIFactory();
            BindGameFactory();
            
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
    }
}