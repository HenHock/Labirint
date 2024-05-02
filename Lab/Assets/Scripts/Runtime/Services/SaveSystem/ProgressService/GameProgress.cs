using System;
using Runtime.Data;

namespace Runtime.Services.SaveSystem.ProgressService
{
    [Serializable]
    public class GameProgress
    {
        public PlayerData m_PlayerData;
        public StatsData m_StatsData;
        public WorldData m_WorldData;
        
        public GameProgress()
        {
            m_StatsData = new StatsData();
            m_PlayerData = new PlayerData();
            m_WorldData = new WorldData();
        }
    }
}