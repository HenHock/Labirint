using Runtime.Services.WindowService;
using UnityEngine;

namespace Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Window", fileName = "WindowConfig")]
    public class WindowConfig : ScriptableObject
    {
        public WindowType m_WindowType;
        public BaseWindow m_WindowPrefab;
    }
}