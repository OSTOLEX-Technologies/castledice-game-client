using Src.Components;
using Src.General.LoadingScenes;
using UnityEngine;

namespace Src.Tutorial
{
    public class TutorialSceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneLoader sceneLoader;

        public void LoadTutorialScene()
        {
            sceneLoader.LoadSceneWithTransition(SceneType.Tutorial);
        }
    }
}
