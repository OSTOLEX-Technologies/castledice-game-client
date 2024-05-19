using System;
using System.Collections.Generic;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class CompositeCommand : TutorialScenarioCommand
    {
        [SerializeField] private List<TutorialScenarioCommand> commands;

        [ContextMenu("Update Commands List")]
        private void UpdateCommandsList()
        {
            commands = new List<TutorialScenarioCommand>();
            var foundCommands = gameObject.GetComponents<TutorialScenarioCommand>();
            foreach (var component in foundCommands)
            {
                if (component != this)
                {
                    commands.Add(component);
                }
            }
        }

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