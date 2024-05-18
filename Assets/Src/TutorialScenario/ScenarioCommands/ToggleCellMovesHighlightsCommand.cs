using Src.GameplayView.CellMovesHighlights;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class ToggleCellMovesHighlightsCommand : TutorialScenarioCommand
    {
        [SerializeField] private UnityCellMovesHighlightsToggle toggle;
        [SerializeField] private bool active;
        
        public override void Do()
        {
            toggle.SetHighlightsActive(active);
        }

        public override void Undo()
        {
            toggle.SetHighlightsActive(!active);
        }
    }
}