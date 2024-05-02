using System;
using UniRx;

namespace Runtime.Data
{
    [Serializable]
    public class StatsData
    {
        public IntReactiveProperty m_TotalAttemptCount;
        public IntReactiveProperty m_WinCount;
        public IntReactiveProperty m_LoseCount;

        public StatsData()
        {
            m_TotalAttemptCount = new();
            m_WinCount = new();
            m_LoseCount = new();
        }
    }
}