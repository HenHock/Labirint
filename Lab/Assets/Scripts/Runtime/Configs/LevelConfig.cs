using Runtime.Configs.Enemy;
using UnityEngine;

namespace Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Level", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Map")]
        public Vector3 m_MapSpawnPoint;
        public GameObject m_MapPrefab;
        
        [Header("Hero")]
        public Vector3 m_HeroSpawnPoint;
        
        [Header("Enemies")]
        public EnemySpawnData[] m_EnemiesSpawnPoints;
    }
}