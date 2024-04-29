using Runtime.Configs;
using UnityEngine;

namespace Runtime.Logic.Gameplay.Spawning
{
    public class EnemySpawnMarker : SpawnMarker
    {
        [SerializeField] private EnemyConfig m_EnemyConfig;

        public EnemyConfig Config => m_EnemyConfig;
        
#if UNITY_EDITOR
        private void OnValidate() => m_MarkerType = SpawnMarkerType.Enemy;

        protected override void OnDrawGizmos()
        {
            if (m_EnemyConfig != null)
            {
                DrawWireMesh(m_EnemyConfig.m_Prefab, Color.red);
                return;
            }
            
            base.OnDrawGizmos();
        }
#endif
    }
}