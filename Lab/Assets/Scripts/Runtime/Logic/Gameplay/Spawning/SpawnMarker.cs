using System;
using Runtime.Configs;
using UnityEngine;

namespace Runtime.Logic.Gameplay.Spawning
{
    public class SpawnMarker : MonoBehaviour
    {
        [SerializeField] protected SpawnMarkerType m_MarkerType;

        public SpawnMarkerType MarkerType => m_MarkerType;

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            switch (m_MarkerType)
            {
                case SpawnMarkerType.Hero:
                    DrawWireMesh(Resources.Load<GameObject>(AssetsPath.Hero), Color.green);
                    break;
                case SpawnMarkerType.Goal:
                    break;
                case SpawnMarkerType.Enemy:
                    DrawWireMesh(Resources.Load<GameObject>(AssetsPath.Enemy), Color.red);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void DrawWireMesh(GameObject obj, Color color)
        {
            if (obj != null)
            {
                Gizmos.color = color;
                Gizmos.DrawWireMesh(obj.GetComponentInChildren<MeshFilter>().sharedMesh, transform.position);
            }
        }
#endif
    }
}