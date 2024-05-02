using System;
using JetBrains.Annotations;
using NaughtyAttributes;
using Runtime.Configs.Infrastructure;
using Runtime.Services.SaveSystem;
using UnityEngine;

namespace Runtime.Logic.Gameplay.Spawning
{
    public class SpawnMarker : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] protected SpawnMarkerType m_MarkerType;
        
        [BoxGroup("CanBeNull")]
        [SerializeField, CanBeNull] protected UniqueID m_UniqueID;

        public string UniqueID => m_UniqueID?.m_ID ?? string.Empty;
        public SpawnMarkerType MarkerType => m_MarkerType;

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            switch (m_MarkerType)
            {
                case SpawnMarkerType.Hero:
                    DrawWireMesh(Resources.Load<GameObject>(AssetsPath.Hero), Color.green);
                    break;
                case SpawnMarkerType.Finish:
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