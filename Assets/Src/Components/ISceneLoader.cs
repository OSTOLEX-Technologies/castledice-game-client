using Src.General.LoadingScenes;
using UnityEngine;

namespace Src.Components
{
    public interface ISceneLoader
    {
        void LoadSceneWithTransition(SceneType sceneType);
        void LoadScene(SceneType sceneType);
        void LoadSceneAdditive(SceneType sceneType);
        AsyncOperation LoadSceneAsync(SceneType sceneType);
        AsyncOperation LoadSceneAsync(string sceneName);
        AsyncOperation LoadSceneAsyncAdditive(SceneType sceneType);
    }
}