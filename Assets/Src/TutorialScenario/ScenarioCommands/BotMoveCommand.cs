using Src.PVE.BotTriggers;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class BotMoveCommand : TutorialScenarioCommand
    {
        [SerializeField] private MonoBehaviourBotMoveTrigger _botMoveTrigger;
        
        public override void Do()
        {
            _botMoveTrigger.TriggerBot();
        }

        public override void Undo()
        {
            Debug.LogWarning("Undoing bot move command is not supported");
        }
    }
}