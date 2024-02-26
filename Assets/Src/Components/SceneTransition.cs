using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Components
{
    public class SceneTransition : MonoBehaviour
    {
        [InspectorName("Scene Loader"), SerializeField] 
        private SceneLoader sceneLoader;
    
        [InspectorName("Loading Bar Slider"), SerializeField] 
        private Slider loadingBarSlider;

        private AsyncOperation _loadingOperation;

        internal static readonly string SceneToLoadPrefName = "SceneToLoadName";
        
        private void Start()
        {
            if (!PlayerPrefs.HasKey(SceneToLoadPrefName))
            {
                throw new InvalidOperationException("Scene to load name pref is not specified, " + gameObject.name);
            }

            var loadingSceneName = PlayerPrefs.GetString(SceneToLoadPrefName);
            _loadingOperation = sceneLoader.LoadSceneAsync(loadingSceneName);
            StartCoroutine(UpdateLoadingProgress());
        }

        private IEnumerator UpdateLoadingProgress()
        {
            while (!_loadingOperation.isDone)
            {
                loadingBarSlider.value = _loadingOperation.progress;
                yield return null;
            }
        }
    }
}
