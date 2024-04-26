using Runtime.Configs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Infrastructure.Bootstrap
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameConfig m_GameConfig;

        private void Awake()
        {
            var bootstrapper = FindAnyObjectByType<GameBootstrapper>(FindObjectsInactive.Include);

            if (bootstrapper == null) 
                SceneManager.LoadScene(m_GameConfig.m_BootstrapScene);
            else Destroy(gameObject);
        }
    }
}