using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Runtime.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public void Load(int sceneID, Action onLoadedAction) => 
            LoadAsync(sceneID, onLoadedAction).Forget();

        private async UniTask LoadAsync(int sceneID, Action onLoadedAction)
        {
            if (SceneManager.GetActiveScene().buildIndex == sceneID)
            {
                onLoadedAction?.Invoke();
                return;
            }

            var asyncOperation = SceneManager.LoadSceneAsync(sceneID);
            await UniTask.WaitUntil(() => asyncOperation.isDone);
            
            onLoadedAction?.Invoke();
        }
    }
}