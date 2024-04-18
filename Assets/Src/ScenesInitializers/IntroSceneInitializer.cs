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
            audioStopEventHandler.AudioPlaybackEnded += LoadAuthScene;
        }

        private void LoadAuthScene(AudioClip clip)
        {
            sceneLoader.LoadSceneAsync(SceneType.Auth);
        }
    }
}