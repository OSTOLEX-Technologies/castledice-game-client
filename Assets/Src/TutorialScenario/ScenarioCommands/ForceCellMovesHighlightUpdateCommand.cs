using Src.GameplayPresenter.CellMovesHighlights;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class ForceCellMovesHighlightUpdateCommand : TutorialScenarioCommand
    {
        [SerializeField] private ForcableCellMovesHighlightObserver forcableCellMovesHighlightObserver;
        
        public override void Do()
        {
            forcableCellMovesHighlightObserver.ForceHighlight();
        }

        public override void Undo()
        {
            Debug.LogWarning("Undo cannot be performed on this command: " + GetType().Name);
        }
    }
}