using System;
using System.Collections.Generic;

namespace Runtime.Data
{
    [Serializable]
    public class WorldData
    {
        public List<TransformData> m_TransformDataList;
        public List<EnemyStateData> m_EnemyDataList;
        public float m_TimerValue;

        public WorldData()
        {
            m_EnemyDataList = new();
            m_TransformDataList = new();
            m_TimerValue = 0;
        }
    }
}