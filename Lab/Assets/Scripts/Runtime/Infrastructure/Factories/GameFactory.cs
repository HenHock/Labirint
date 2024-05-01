using System.Linq;
using Cinemachine;
using Runtime.Configs;
using Runtime.Configs.Infrastructure;
using Runtime.Logic.Gameplay.Enemy.AIStateMachine;
using Runtime.Services.Providers.AssetsProvider;
using Runtime.Services.Providers.ConfigsProvider;
using Runtime.Services.Save;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Factories
{
    public sealed class GameFactory : BaseFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly IPersistentProgressService _progressService;

        private LevelConfig _currentLevelConfig;
        private Transform _hero;

        public LevelConfig CurrentLevelConfig => _currentLevelConfig == null
            ? _currentLevelConfig = GetLevelConfig(_progressService.Progress.m_CompletedLevels.Value)
            : _currentLevelConfig;
        
        public GameFactory(DiContainer container, IAssetProvider assetProvider, IConfigProvider configProvider, IPersistentProgressService progressService) 
            : base(container, assetProvider)
        {
            _configProvider = configProvider;
            _progressService = progressService;
        }

        public override void Cleanup()
        {
            _currentLevelConfig = null;
        }

        public void CreateMap()
        {
            Debug.Assert(CurrentLevelConfig.m_MapPrefab != null, $"{CurrentLevelConfig.name} doesn't have assign map prefab");
            InstantiatePrefab(CurrentLevelConfig.m_MapPrefab, CurrentLevelConfig.m_MapSpawnPoint);
        }

        public void CreateHero() => 
            _hero = InstantiateAsset(AssetProviderKey.Hero, CurrentLevelConfig.m_HeroSpawnPoint, Quaternion.identity).transform;

        public void CreateFollowCamera()
        {
            var forwardCamera = InstantiateAsset<CinemachineVirtualCamera>();
            forwardCamera.m_Follow = _hero;
            forwardCamera.m_LookAt = _hero;
        }

        public void CreateEnemies()
        {
            foreach (var enemySpawnData in CurrentLevelConfig.m_EnemiesSpawnPoints)
            {
                var enemyInstance = InstantiatePrefab(enemySpawnData.m_Config.m_Prefab, enemySpawnData.m_Position, Quaternion.identity);
                var enemyAI = enemyInstance.GetComponent<EnemyAI>();
                
                enemyAI.Construct(enemySpawnData.m_Config, enemySpawnData.m_Waypoints);
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
    }
}