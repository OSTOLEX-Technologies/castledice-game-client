using Src.Components;
using Src.LoadingScenes;
using UnityEngine;
using UnityEngine.Video;

namespace Src.ScenesInitializers
{
    public class CinematicSceneInitializer: MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private SceneLoader sceneLoader;
        
        private void Awake()
        {
            videoPlayer.loopPointReached += EndReached;
            if (PlayerPrefs.GetInt("CinematicViewed", 0) == 1)
            {
                EndReached(videoPlayer);
            }
        }
        
        private void EndReached(VideoPlayer vp)
        {
            vp.Stop();
            PlayerPrefs.SetInt("CinematicViewed", 1);
            GoToNextScene();
        }

        private void GoToNextScene()
        {
            if (PlayerPrefs.GetInt("TutorialPassed", 0) == 0)
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