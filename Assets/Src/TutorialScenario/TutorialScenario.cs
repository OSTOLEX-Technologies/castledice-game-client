using System;
using System.Collections.Generic;
using Src.TutorialScenario.ScenarioFrames;
using UnityEngine;

namespace Src.TutorialScenario
{
    public class TutorialScenario : MonoBehaviour
    {
        [SerializeField] private List<ScenarioFrame> frames;
        private int _frameIndex;

        public event Action FramesQueueEnded;

        
        private void Awake()
        {
            frames[_frameIndex].frameChangeTrigger.NextFrameRequested += OnNextFrameRequested;
            frames[_frameIndex].command.Do();
        }

        public void Undo()
        {
            if (_frameIndex == 0)
            {
                Debug.LogError("Trying to undo the first one frame in a sequence");
                return;
            }
            
            frames[_frameIndex].frameChangeTrigger.NextFrameRequested -= OnNextFrameRequested;
            
            frames[_frameIndex--].command.Undo();
            frames[_frameIndex].frameChangeTrigger.NextFrameRequested += OnNextFrameRequested;
            Debug.Log("Undo frame to " + frames[_frameIndex].command.gameObject.name);
        }
        
        private void OnNextFrameRequested()
        {
            Debug.Log("Next frame (" + frames[_frameIndex].command.gameObject.name + ") started");
            
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
