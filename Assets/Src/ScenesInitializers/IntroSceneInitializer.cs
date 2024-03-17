using Src.Components;
using Src.LoadingScenes;
using UnityEngine;

namespace Src.ScenesInitializers
{
    public class IntroSceneInitializer : MonoBehaviour
    {
        [SerializeField, InspectorName("Scene Loader")]
        private SceneLoader sceneLoader;
        
        [SerializeField, InspectorName("Audio Stop Event Handler")]
        private AudioStopEventHandler audioStopEventHandler;

        private void Awake()
        {
            audioStopEventHandler.AudioPlaybackEnded += LoadNextScene;
        }

        private void LoadNextScene(AudioClip clip)
        {
            if (PlayerPrefs.GetInt("CinematicViewed", 0) == 0)
            {
                sceneLoader.LoadSceneAsync(SceneType.Cinematic);
            } else if (PlayerPrefs.GetInt("TutorialPassed", 0) == 0)
            {
                sceneLoader.LoadSceneWithTransition(SceneType.Tutorial);
            }
            else 
            {
                sceneLoader.LoadSceneWithTransition(SceneType.Auth);
            }
        }
    }
}