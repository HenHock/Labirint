using UnityEngine;

namespace Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Enemy", fileName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [Header("Spawning")] 
        public GameObject m_Prefab;
        
        [Header("Movement")]
        public float m_Speed = 10;

        [Header("View")]
        [Range(30, 360)]
        public int m_ViewAngle = 30;
        public float m_ViewDistance = 5;
    }
}