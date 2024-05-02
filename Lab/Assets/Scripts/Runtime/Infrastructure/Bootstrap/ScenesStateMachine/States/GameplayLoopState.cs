using System.Threading.Tasks;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces;
using Runtime.Infrastructure.Factories;
using Runtime.Services.Input;
using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.SaveSystem.SaveLoadService;
using UnityEngine;

namespace Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States
{
    public sealed class GameplayLoopState : IState
    {
        private readonly IStateMachine _sceneStateMachine;
        private readonly IConfigProvider _configProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly GameFactory _gameFactory;
        private readonly UIFactory _uiFactory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IInputService _inputService;

        public GameplayLoopState(SceneStateMachine sceneStateMachine, IConfigProvider configProvider, IAssetProvider assetProvider, 
            GameFactory gameFactory, UIFactory uiFactory, ISaveLoadService saveLoadService, IInputService inputService)
        {
            _sceneStateMachine = sceneStateMachine;
            _configProvider = configProvider;
            _assetProvider = assetProvider;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _saveLoadService = saveLoadService;
            _inputService = inputService;
        }

        public void Enter()
        {
            _configProvider.LoadConfigs(ContextType.Gameplay);
            _assetProvider.LoadAssets(ContextType.Gameplay);
            
            InitLevelWorld();
            InitUI();
            ShowFinishZone().Forget();
        }

        public void Exit()
        {
            _configProvider.UnloadConfigs(ContextType.Gameplay);
            _assetProvider.UnloadAssets(ContextType.Gameplay);
            
            _gameFactory.Cleanup();
            _uiFactory.Cleanup();
            
            _saveLoadService.Save();
            _saveLoadService.Cleanup();
        }
        
        private void InitLevelWorld()
        {
            _gameFactory.CreateMap();
            _gameFactory.CreateHero();
            _gameFactory.CreateEnemies();
            _gameFactory.CreateFinishZone();
            _gameFactory.CreateFollowCamera();
        }

        private void InitUI()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateGameplayHUD();
        }
        
        private async UniTaskVoid ShowFinishZone()
        {
            var finishCamera = _gameFactory.FinishZone.GetComponentInChildren<CinemachineVirtualCamera>(true);
            var brain = Camera.main.GetComponent<CinemachineBrain>();
            
            // Wanting a frame to renderer the first frame.
            await UniTask.DelayFrame(1);
            
            _inputService.DisableInputs();
            
            finishCamera.gameObject.SetActive(true);
            await WaitingCameraBlending(brain);
            
            finishCamera.gameObject.SetActive(false);
            await WaitingCameraBlending(brain);
            
            _inputService.EnableInputs();
        }

        private async UniTask WaitingCameraBlending(CinemachineBrain brain) => 
            await UniTask.Delay(Mathf.RoundToInt(brain.m_DefaultBlend.BlendTime * 1000));
    }
}