using Src.ScenesInitializers;
using Src.TutorialScenario.FrameChangeTrigger;
using UnityEngine;

namespace Src.Tutorial
{
    public class TutorialInitializationListener : FrameChangeTriggerBase
    {
        [SerializeField] private TutorialSceneInitializer initializer;

        private void Awake()
        {
            initializer.InitializationFinished += OnInitializationFinished;
        }

        private void OnInitializationFinished()
        {
            RequestNextFrame();
        }
    }
}