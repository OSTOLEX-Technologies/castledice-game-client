using System.Collections;
using UnityEngine;

namespace Src.TutorialScenario.FrameChangeTrigger
{
    public class FirstFrameTriggerDelaySample : FrameChangeTriggerBase
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(3f);
            RequestNextFrame();
        }
    }
}