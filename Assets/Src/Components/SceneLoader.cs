using UnityEngine;
using UnityEngine.SceneManagement;

namespace Src.Components
{
    public class SceneLoader : MonoBehaviour
    {
        private static string TransitionSceneName => "Transition";

        #region Transition Scene Loading
        public void LoadSceneWithTransition(string sceneName)
        {
            UpdateTargetLoadScenePrefName(sceneName);
            LoadScene(TransitionSceneName);
        }
        
        private void UpdateTargetLoadScenePrefName(string sceneName)
        {
            PlayerPrefs.SetString(SceneTransition.SceneToLoadPrefName, sceneName);
        }
        #endregion
        

        #region Loading Methods
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public void LoadSceneAdditive(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        public AsyncOperation LoadSceneAsync(string sceneName)
        {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

        public AsyncOperation LoadSceneAsyncAdditive(string sceneName)
        {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        #endregion
    }
}
