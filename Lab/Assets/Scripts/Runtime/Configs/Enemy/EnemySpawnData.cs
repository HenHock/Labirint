using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Runtime.Configs.Enemy
{
    [Serializable]
    public struct EnemySpawnData
    {
        [ReadOnly, AllowNesting] 
        public string m_UniqueID;

        public EnemyConfig m_Config;
        public Vector3 m_Position;
        public Vector3[] m_Waypoints;

        public EnemySpawnData(EnemyConfig enemyConfig, Vector3 position, Vector3[] waypoints, string uniqueID)
        {
            m_Config = enemyConfig;
            m_Position = position;
            m_Waypoints = waypoints;
            m_UniqueID = uniqueID;

            if (!m_Waypoints.Any()) 
                m_Waypoints = new[] { position };
        }
    }
}