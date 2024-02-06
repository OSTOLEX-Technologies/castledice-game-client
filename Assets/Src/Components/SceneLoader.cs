using Src.LoadingScenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Src.Components
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField, InspectorName("LoadingScenes Config")]
        private LoadingScenesConfig loadingScenesConfig;

        #region Transition Scene Loading
        public void LoadSceneWithTransition(ESceneType sceneType)
        {
            var loadingSceneName = ResolveSceneType(sceneType);
            PlayerPrefs.SetString(SceneTransition.SceneToLoadPrefName, loadingSceneName);
            LoadScene(loadingScenesConfig.TransitionSceneName);
        }
        
        private string ResolveSceneType(ESceneType sceneType)
        {
            return loadingScenesConfig.GetSceneName(sceneType);
        }
        #endregion
        

        #region Public Loading Methods
        public void LoadScene(ESceneType sceneType)
        {
            LoadScene(ResolveSceneType(sceneType));
        }

        public void LoadSceneAdditive(ESceneType sceneType)
        {
            LoadSceneAdditive(ResolveSceneType(sceneType));
        }

        public AsyncOperation LoadSceneAsync(ESceneType sceneType)
        {
            return LoadSceneAsync(ResolveSceneType(sceneType));
        }

        public AsyncOperation LoadSceneAsyncAdditive(ESceneType sceneType)
        {
            return LoadSceneAsyncAdditive(ResolveSceneType(sceneType));
        }
        #endregion


        #region Low Level Loading Methods
        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        private void LoadSceneAdditive(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        public AsyncOperation LoadSceneAsync(string sceneName)
        {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

        private AsyncOperation LoadSceneAsyncAdditive(string sceneName)
        {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        #endregion
    }
}
