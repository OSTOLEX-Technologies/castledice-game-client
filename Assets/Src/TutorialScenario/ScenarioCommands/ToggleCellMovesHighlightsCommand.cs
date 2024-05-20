using Src.GameplayView.CellMovesHighlights;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class ToggleAllCellMovesHighlightsCommand : TutorialScenarioCommand
    {
        [SerializeField] private UnityCellMovesHighlightsToggle toggle;
        [SerializeField] private bool active;
        
        public override void Do()
        {
            toggle.ToggleAllHighlights(active);
        }

        public override void Undo()
        {
            toggle.ToggleAllHighlights(!active);
        }
    }
}