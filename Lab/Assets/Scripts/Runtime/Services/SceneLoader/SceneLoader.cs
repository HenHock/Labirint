﻿using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public void Load(int sceneID, Action onLoadedAction) => 
            LoadAsync(sceneID, onLoadedAction).Forget();

        private async UniTask LoadAsync(int sceneID, Action onLoadedAction)
        {
            Debug.Log($"{GetType().Name}: Start loading the {sceneID} scene");
            
            if (SceneManager.GetActiveScene().buildIndex == sceneID)
            {
                onLoadedAction?.Invoke();
                return;
            }

            var asyncOperation = SceneManager.LoadSceneAsync(sceneID);
            await UniTask.WaitUntil(() => asyncOperation.isDone);
            
            onLoadedAction?.Invoke();
            Debug.Log($"{GetType().Name}: Finished loading the {sceneID} scene");
        }
    }
}