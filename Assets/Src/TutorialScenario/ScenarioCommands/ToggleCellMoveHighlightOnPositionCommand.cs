using Src.GameplayView.CellMovesHighlights;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class ToggleCellMoveHighlightOnPositionCommand : TutorialScenarioCommand
    {
        [SerializeField] private Vector2Int position;
        [SerializeField] private bool active;
        [SerializeField] private UnityCellMovesHighlightsToggle toggle;
        
        public override void Do()
        {
            toggle.ToggleHighlight((position.x, position.y), active);
        }

        public override void Undo()
        {
            toggle.ToggleHighlight((position.x, position.y), !active);
        }
    }
}