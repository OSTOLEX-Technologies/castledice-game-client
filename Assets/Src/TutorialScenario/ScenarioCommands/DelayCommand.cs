using System.Collections;
using Src.TutorialScenario.FrameChangeTrigger;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class DelayCommand : TutorialScenarioCommand
    {
        [SerializeField] private float delayInSeconds;
        [SerializeField] private ExternalUnityFrameChangeTrigger delayEndTrigger;
        private Coroutine _cachedTimerCoroutine;
        
        public override void Do()
        {
            _cachedTimerCoroutine = StartCoroutine(Timer(delayInSeconds));
        }

        public override void Undo()
        {
            if (_cachedTimerCoroutine is not null)
            {
                StopCoroutine(_cachedTimerCoroutine);
            }
        }

        private IEnumerator Timer(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            delayEndTrigger.GoToNextFrame();
        }
    }
}