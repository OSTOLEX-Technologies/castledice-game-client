using System;
using Src.TutorialScenario.FrameChangeTrigger;
using Src.TutorialScenario.ScenarioCommands;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioFrames
{
    [Serializable]
    public struct ScenarioFrame
    {
        [Header("Command")]
        [SerializeField] public TutorialScenarioCommand command;
        [Header("Next Frame Trigger")]
        [SerializeField] public FrameChangeTriggerBase frameChangeTrigger;
    }
}