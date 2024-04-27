using System;
using UnityEngine;

namespace Runtime.Configs
{
    [Serializable]
    public struct EnemySpawnData
    {
        public EnemyConfig m_Config;
        public Vector3 m_Position;
    }
}