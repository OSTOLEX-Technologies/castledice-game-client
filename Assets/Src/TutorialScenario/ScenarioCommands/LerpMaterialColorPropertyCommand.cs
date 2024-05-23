using System;
using System.Collections;
using Src.TutorialScenario.FrameChangeTrigger;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class LerpMaterialColorPropertyCommand : TutorialScenarioCommand
    {
        [SerializeField] private Material materialToModify;
        [SerializeField] private string propertyToModify;
        [SerializeField] private Color startColor;
        [SerializeField] private Color endColor;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float lerpTimeInSeconds;
        [SerializeField] private float afterWaitTimeInSeconds;
        [SerializeField] private bool lerpBack;

        [SerializeField] private ExternalUnityFrameChangeTrigger lerpEndTrigger;

        private event Action _lerpCompleted;

        public override void Do()
        {
            _lerpCompleted += OnLerpCompleted;
            StartCoroutine(LerpCycle(startColor, endColor));
        }

        private void OnLerpCompleted()
        {
            _lerpCompleted -= OnLerpCompleted;
            lerpEndTrigger.GoToNextFrame();
        }

        public override void Undo()
        {
            StartCoroutine(LerpCycle(endColor, startColor));
        }

        private IEnumerator LerpCycle(Color begin, Color end)
        {
            yield return StartCoroutine(LerpColor(begin, end));
            
            yield return new WaitForSecondsRealtime(afterWaitTimeInSeconds);
            
            if (lerpBack)
            {
                yield return StartCoroutine(LerpColor(end, begin)); 
            }
            
            _lerpCompleted?.Invoke();
        }

        private IEnumerator LerpColor(Color begin, Color end)
        {
            var timer = 0f;
            while(timer < lerpTimeInSeconds)
            {
                materialToModify.SetColor(
                    propertyToModify,
                    Color.Lerp(
                        begin,
                        end,
                        curve.Evaluate(timer / lerpTimeInSeconds)));

                yield return null;
                timer += Time.deltaTime;
            }
        }
    }
}