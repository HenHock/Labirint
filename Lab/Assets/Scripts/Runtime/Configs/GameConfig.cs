using UnityEngine;

namespace Runtime.Configs
{
    /// <summary>
    /// Config with global game settings.
    /// </summary>
    [CreateAssetMenu(menuName = "Configs/GameConfig", fileName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public int m_BootstrapScene = 0;
        // public int m_MenuScene = 1;
        public int m_GameplayScene = 1;
    }
}