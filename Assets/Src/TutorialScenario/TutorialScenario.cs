using System;
using System.Collections.Generic;
using Src.TutorialScenario.FrameChangeTrigger;
using Src.TutorialScenario.ScenarioFrames;
using UnityEngine;

namespace Src.TutorialScenario
{
    public class TutorialScenario : MonoBehaviour
    {
        [SerializeField] private FrameChangeTriggerBase firstFrameTrigger;
        [SerializeField] private List<ScenarioFrame> frames;
        private int _frameIndex;

        public event Action FramesQueueEnded;

        
        private void Start()
        {
            firstFrameTrigger.NextFrameRequested += OnFirstFrameRequested;
        }

        private void OnFirstFrameRequested()
        {
            firstFrameTrigger.NextFrameRequested -= OnFirstFrameRequested;

            frames[_frameIndex].frameChangeTrigger.NextFrameRequested += OnNextFrameRequested;
            frames[_frameIndex].command.Do();
        }
        private void OnNextFrameRequested()
        {
            frames[_frameIndex].frameChangeTrigger.NextFrameRequested -= OnNextFrameRequested;
            
            if (++_frameIndex >= frames.Count)
            {
                FramesQueueEnded?.Invoke();
                return;
            }

            frames[_frameIndex].frameChangeTrigger.NextFrameRequested += OnNextFrameRequested;
            frames[_frameIndex].command.Do();
        }
    }
}
