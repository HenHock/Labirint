using System;

namespace Runtime.Services.SceneLoader
{
    public interface ISceneLoader
    {
        void Load(int sceneID, Action onLoadedAction = null);
    }
}