using System;
using UniRx;

namespace Runtime.Services.Save
{
    [Serializable]
    public class GameProgress
    {
        public IntReactiveProperty m_CompletedLevels;
        
        public GameProgress()
        {
            m_CompletedLevels = new(0);
        }
    }
}