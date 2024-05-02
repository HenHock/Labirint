using System;
using UniRx;

namespace Runtime.Data
{
    [Serializable]
    public class PlayerData
    {
        public IntReactiveProperty m_CompletedLevels;

        public PlayerData()
        {
            m_CompletedLevels = new();
        }
    }
}