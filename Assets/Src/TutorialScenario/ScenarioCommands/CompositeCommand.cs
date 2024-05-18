using System.Collections.Generic;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class CompositeCommand : TutorialScenarioCommand
    {
        [SerializeField] private List<TutorialScenarioCommand> commands;

        public override void Do()
        {
            foreach (var command in commands)
            {
                command.Do();
            }
        }

        public override void Undo()
        {
            for (var i = commands.Count - 1; i >= 0; i--)
            {
                commands[i].Undo();
            }
        }
    }
}