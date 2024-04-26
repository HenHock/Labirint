using System;
using UniRx;

namespace Runtime.Services.Save
{
    [Serializable]
    public class GameProgress
    {
        public ReactiveProperty<int> m_EarningPoints;
        public ReactiveProperty<int> m_TraveledDistance;

        public GameProgress()
        {
            m_EarningPoints = new();
            m_TraveledDistance = new();
        }
    }
}