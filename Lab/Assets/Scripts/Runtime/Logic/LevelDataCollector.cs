using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Runtime.Configs;
using Runtime.Configs.Enemy;
using Runtime.Logic.Gameplay.Spawning;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Runtime.Logic
{
#if UNITY_EDITOR
    public class LevelDataCollector : MonoBehaviour
    {
        [Expandable]
        [InfoBox("To collect, the object must be on the scene", EInfoBoxType.Warning)]
        [SerializeField] private LevelConfig m_LevelConfig;

        [Button(nameof(Collect), EButtonEnableMode.Editor)]
        private void Collect()
        {
            var spawnMarkers = FindObjectsByType<SpawnMarker>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            CollectMapData();
            CollectHeroData(spawnMarkers);
            CollectEnemiesData(spawnMarkers);
            
            EditorUtility.SetDirty(m_LevelConfig);
        }

        private void CollectMapData()
        {
            var map = GameObject.FindGameObjectWithTag(TagConfig.Map);

            if (map != null)
            {
                m_LevelConfig.m_MapSpawnPoint = map.transform.position;
                m_LevelConfig.m_MapPrefab = map.gameObject.GetPrefabDefinition().GameObject();
            }
        }

        private void CollectHeroData(SpawnMarker[] spawnMarkers) => 
            m_LevelConfig.m_HeroSpawnPoint = spawnMarkers.First(marker => marker.MarkerType == SpawnMarkerType.Hero).transform.position;

        private void CollectEnemiesData(SpawnMarker[] spawnMarkers) => 
            m_LevelConfig.m_EnemiesSpawnPoints = spawnMarkers
                .Where(marker => marker.MarkerType == SpawnMarkerType.Enemy)
                .Cast<EnemySpawnMarker>()
                .Where(marker => marker != null)
                .Select(marker => new EnemySpawnData(marker.Config, marker.transform.position, marker.Waypoints))
                .ToArray();
    }
#endif
}