using System.Collections;

namespace Src.TutorialScenario.FrameChangeTrigger
{
    public class OneFrameDelayTriggerSample : FrameChangeTriggerBase
    {
        private IEnumerator Start()
        {
            yield return null;
            RequestNextFrame();
        }
    }
}