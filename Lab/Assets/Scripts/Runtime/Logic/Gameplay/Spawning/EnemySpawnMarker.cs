using System;
using Runtime.Configs;
using Runtime.Configs.Enemy;
using Runtime.Logic.Gameplay.Enemy;
using UnityEngine;

namespace Runtime.Logic.Gameplay.Spawning
{
    public class EnemySpawnMarker : SpawnMarker
    {
        [SerializeField] private EnemyConfig m_EnemyConfig;
        [SerializeField] private VisionCone m_VisionCone;
        [SerializeField] private Transform m_PatrollingPath;

        public EnemyConfig Config => m_EnemyConfig;

        public Vector3[] Waypoints
        {
            get
            {
                if (m_PatrollingPath != null)
                {
                    var waypoints = new Vector3[m_PatrollingPath.transform.childCount];
                    for (int i = 0; i < m_PatrollingPath.transform.childCount; i++)
                        waypoints[i] = m_PatrollingPath.transform.GetChild(i).position;

                    return waypoints;
                }

                return Array.Empty<Vector3>();
            }
        }

#if UNITY_EDITOR

        private void Reset()
        {
            m_MarkerType = SpawnMarkerType.Enemy;
            
            if (m_VisionCone != null)
            {
                m_VisionCone.m_VisionRange = m_EnemyConfig.m_ViewDistance;
                m_VisionCone.m_VisionAngle = m_EnemyConfig.m_ViewAngle;
            }
        }

        private void OnValidate() => Reset();

        private new void OnDrawGizmos()
        {
            if (m_EnemyConfig != null)
            {
                DrawWireMesh(m_EnemyConfig.m_Prefab, Color.red);
                DrawPatrollingPath(Waypoints);
            }
        }

        private void DrawPatrollingPath(Vector3[] waypoints)
        {
            for (var index = 1; index < waypoints.Length; index++)
            {
                Gizmos.DrawLine(waypoints[index - 1], waypoints[index]);
                Gizmos.DrawSphere(waypoints[index], 0.25f);
            }
        }
#endif
    }
}