using UnityEngine;

namespace Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Hero", fileName = "HeroConfig")]
    public class HeroConfig : ScriptableObject
    {
        [Header("Movement")]
        public float m_Speed = 10;
        public float m_RotationSpeed = 5;
    }
}