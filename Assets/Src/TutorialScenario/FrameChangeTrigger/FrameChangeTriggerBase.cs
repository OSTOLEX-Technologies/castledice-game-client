using System;
using UnityEngine;

namespace Src.TutorialScenario.FrameChangeTrigger
{
    public abstract class FrameChangeTriggerBase : MonoBehaviour
    {
        public event Action NextFrameRequested;

        protected void RequestNextFrame()
        {
            NextFrameRequested?.Invoke();
        }
    }
}