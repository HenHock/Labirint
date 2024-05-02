using System.Linq;
using Cinemachine;
using Runtime.Configs;
using Runtime.Configs.Infrastructure;
using Runtime.Configs.Level;
using Runtime.Logic;
using Runtime.Logic.Gameplay;
using Runtime.Logic.Gameplay.Enemy;
using Runtime.Logic.Gameplay.Enemy.AIStateMachine;
using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.SaveSystem;
using Runtime.Services.SaveSystem.ProgressService;
using Runtime.Services.SaveSystem.SaveLoadService;
using Runtime.Services.WindowService;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Factories
{
    public sealed class GameFactory : BaseFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly IPersistentProgressService _progressService;

        private LevelConfig _currentLevelConfig;

        public LevelConfig CurrentLevelConfig => _currentLevelConfig == null
            ? _currentLevelConfig = GetLevelConfig(_progressService.Progress.m_PlayerData.m_CompletedLevels.Value)
            : _currentLevelConfig;

        public Transform Hero { get; private set; }
        public CinemachineVirtualCamera FollowCamera { get; private set; }
        public Transform FinishZone { get; private set; }

        public GameFactory(DiContainer container, IAssetProvider assetProvider, IConfigProvider configProvider, 
            IPersistentProgressService progressService, ISaveLoadService saveLoadService) 
            : base(container, assetProvider, saveLoadService)
        {
            _configProvider = configProvider;
            _progressService = progressService;
        }

        public override void Cleanup()
        {
            _currentLevelConfig = null;
            
            FinishZone = null;
            FollowCamera = null;
            Hero = null;
        }

        public void CreateMap()
        {
            Debug.Assert(CurrentLevelConfig.m_MapPrefab != null, $"{CurrentLevelConfig.name} doesn't have assign map prefab");
            InstantiatePrefab(CurrentLevelConfig.m_MapPrefab, CurrentLevelConfig.m_MapSpawnPoint);
        }

        public void CreateHero()
        {
            Hero = InstantiateAsset(AssetProviderKey.Hero, CurrentLevelConfig.m_HeroSpawnPoint, Quaternion.identity).transform;
            SyncUniqueID(Hero.gameObject, CurrentLevelConfig.m_HeroUniqueID);
        }

        public void CreateFinishZone() => FinishZone = InstantiateAsset<FinishZone>(position: CurrentLevelConfig.m_FinishPosition).transform;

        public void CreateFollowCamera()
        {
            FollowCamera = InstantiateAsset<CinemachineVirtualCamera>();
            FollowCamera.m_Follow = Hero;
            FollowCamera.m_LookAt = Hero;
        }

        public void CreateEnemies()
        {
            foreach (var enemySpawnData in CurrentLevelConfig.m_EnemiesSpawnPoints)
            {
                var enemyInstance = InstantiatePrefab(enemySpawnData.m_Config.m_Prefab, enemySpawnData.m_Position, Quaternion.identity);
                var enemyAI = enemyInstance.GetComponent<EnemyAI>();

                enemyAI.Construct(enemySpawnData.m_Config, enemySpawnData.m_Waypoints, Container.Resolve<IWindowService>());
                SyncUniqueID(enemyInstance, enemySpawnData.m_UniqueID);
            }
        }

        private LevelConfig GetLevelConfig(int level)
        {
            var levelConfigs = _configProvider.Get<LevelConfig[]>();
            Debug.Assert(levelConfigs.Any(), "Couldn't find any level config.");

            if (level < 0 || level >= levelConfigs.Length)
            {
                level = 0;
                Debug.LogError($"Couldn't find the {level} level data. The first level will be loaded.");
            }

            return levelConfigs[level];
        }

        private void SyncUniqueID(GameObject instance, string uniqueIDValue)
        {
            foreach (var uniqueID in instance.GetComponentsInChildren<UniqueID>())
                uniqueID.m_ID = uniqueIDValue;
        }
    }
}