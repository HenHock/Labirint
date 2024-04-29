using System.Linq;
using NaughtyAttributes;
using Runtime.Logic.Gameplay.Spawning;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Configs
{
#if UNITY_EDITOR
    public class LevelDataCollector : MonoBehaviour
    {
        [Expandable]
        [SerializeField] private LevelConfig m_LevelConfig;

        [Button]
        private void Collect()
        {
            var spawnMarkers = FindObjectsByType<SpawnMarker>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            CollectMapData();
            CollectHeroData(spawnMarkers);
            CollectEnemiesData(spawnMarkers);
        }

        private void CollectMapData()
        {
            var map = GameObject.FindGameObjectWithTag(TagConfig.Map);
            
            m_LevelConfig.m_MapSpawnPoint = map.transform.position;
            m_LevelConfig.m_MapPrefab = map.gameObject.GetPrefabDefinition().GameObject();
        }

        private void CollectHeroData(SpawnMarker[] spawnMarkers) => 
            m_LevelConfig.m_HeroSpawnPoint = spawnMarkers.First(marker => marker.MarkerType == SpawnMarkerType.Hero).transform.position;

        private void CollectEnemiesData(SpawnMarker[] spawnMarkers) => 
            m_LevelConfig.m_EnemiesSpawnPoints = spawnMarkers
                .Where(marker => marker.MarkerType == SpawnMarkerType.Enemy)
                .Cast<EnemySpawnMarker>()
                .Where(marker => marker != null)
                .Select(marker => new EnemySpawnData(marker.Config, marker.transform.position))
                .ToArray();
    }
#endif
}