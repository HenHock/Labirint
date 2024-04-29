using Runtime.Configs;
using Runtime.Logic.Gameplay.Enemy;
using UnityEngine;

namespace Runtime.Logic.Gameplay.Spawning
{
    public class EnemySpawnMarker : SpawnMarker
    {
        [SerializeField] private EnemyConfig m_EnemyConfig;
        [SerializeField] private VisionCone m_VisionCone;

        public EnemyConfig Config => m_EnemyConfig;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            m_MarkerType = SpawnMarkerType.Enemy;
            
            if (m_VisionCone != null)
            {
                m_VisionCone.m_VisionRange = m_EnemyConfig.m_ViewDistance;
                m_VisionCone.m_VisionAngle = m_EnemyConfig.m_ViewAngle;
            }
        }

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