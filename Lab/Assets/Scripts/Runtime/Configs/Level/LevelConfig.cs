using NaughtyAttributes;
using Runtime.Configs.Enemy;
using UnityEngine;

namespace Runtime.Configs.Level
{
    [CreateAssetMenu(menuName = "Configs/Level", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Map")]
        public GameObject m_MapPrefab;
        public Vector3 m_MapSpawnPoint;
        
        [Header("Completing level")]
        public float m_SecondsToCompleteLevel;
        public Vector3 m_FinishPosition;
        
        [Header("Hero")]
        public Vector3 m_HeroSpawnPoint;
        [ReadOnly] public string m_HeroUniqueID;
        
        [Header("Enemies")]
        public EnemySpawnData[] m_EnemiesSpawnPoints;

    }
}