using System;
using Src.TutorialScenario.FrameChangeTrigger;
using Src.TutorialScenario.ScenarioCommands;

namespace Src.TutorialScenario.ScenarioFrames
{
    [Serializable]
    public struct ScenarioFrame
    {
        public TutorialScenarioCommand command;
        public FrameChangeTriggerBase frameChangeTrigger;
    }
}