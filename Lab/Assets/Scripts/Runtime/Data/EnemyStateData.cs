using System;

namespace Runtime.Data
{
    [Serializable]
    public class EnemyStateData
    {
        public string m_UniqueID;
        public string m_State;
        public string m_StateProgress;

        public EnemyStateData(string uniqueID, string state, string stateProgress)
        {
            m_UniqueID = uniqueID;
            m_State = state;
            m_StateProgress = stateProgress;
        }
    }
}