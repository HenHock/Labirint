using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Services.SceneLoader
{
    public sealed class SceneLoader : ISceneLoader
    {
        public void Load(int sceneID, Action onLoadedAction) => 
            LoadAsync(sceneID, onLoadedAction).Forget();

        private async UniTask LoadAsync(int sceneID, Action onLoadedAction = null)
        {
            Debug.Log($"{GetType().Name}: Start loading the {sceneID} scene");

            var asyncOperation = SceneManager.LoadSceneAsync(sceneID);
            await UniTask.WaitUntil(() => asyncOperation.isDone);
            
            onLoadedAction?.Invoke();
            Debug.Log($"{GetType().Name}: Finished loading the {sceneID} scene");
        }
    }
}