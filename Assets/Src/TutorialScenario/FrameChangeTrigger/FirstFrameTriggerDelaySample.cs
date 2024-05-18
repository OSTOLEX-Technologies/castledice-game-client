using System.Collections;

namespace Src.TutorialScenario.FrameChangeTrigger
{
    public class FirstFrameTriggerDelaySample : FrameChangeTriggerBase
    {
        private IEnumerator Start()
        {
            yield return null;
            RequestNextFrame();
        }
    }
}