using System.Linq;
using NaughtyAttributes;
using Runtime.Configs.Enemy;
using Runtime.Logic.Gameplay.Spawning;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Runtime.Configs.Level
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
            CollectFinishData(spawnMarkers);
            
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

        private void CollectHeroData(SpawnMarker[] spawnMarkers)
        {
            var heroMarker = GetFirstMarker(spawnMarkers, SpawnMarkerType.Hero);
            m_LevelConfig.m_HeroSpawnPoint = heroMarker.transform.position;
            m_LevelConfig.m_HeroUniqueID = heroMarker.UniqueID;
        }

        private void CollectEnemiesData(SpawnMarker[] spawnMarkers) => 
            m_LevelConfig.m_EnemiesSpawnPoints = spawnMarkers
                .Where(marker => marker.MarkerType == SpawnMarkerType.Enemy)
                .Cast<EnemySpawnMarker>()
                .Where(marker => marker != null)
                .Select(marker => new EnemySpawnData(marker.Config, marker.transform.position, marker.Waypoints, marker.UniqueID))
                .ToArray();

        private void CollectFinishData(SpawnMarker[] spawnMarkers) => 
            m_LevelConfig.m_FinishPosition = GetFirstMarker(spawnMarkers, SpawnMarkerType.Finish).transform.position;

        private SpawnMarker GetFirstMarker(SpawnMarker[] spawnMarkers, SpawnMarkerType markerType) => 
            spawnMarkers.First(marker => marker.MarkerType == markerType);
    }
#endif
}