using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;
using Src.Tutorial.HintPointer;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class TogglePointerOnGridCommand : TutorialScenarioCommand
    {
        private enum PointerAction
        {
            Enable,
            Disable,
        }
        
        [SerializeField] private MonoBehaviourSquareGridGenerator gridGenerator;
        [SerializeField] private Vector2Int position;
        [SerializeField] private HintPointerPool pointerPool;
        [SerializeField] private PointerAction pointerAction;
        
        private HintPointer _cachedPointer;
        private IGridCell _gridCell;

        private void Awake()
        {
            gridGenerator.GridGenerated += OnGridGenerated;
        }

        private void OnGridGenerated(IGrid grid)
        {
            gridGenerator.GridGenerated -= OnGridGenerated;
            _gridCell = grid.GetCell((position.x, position.y));
        }

        public override void Do()
        {
            if (pointerAction.Equals(PointerAction.Enable))
            {
                AddPointer();
            }
            else
            {
                RemovePointer();
            }
        }

        public override void Undo()
        {
            if (pointerAction.Equals(PointerAction.Enable))
            {
                RemovePointer();
            }
            else
            {
                AddPointer();
            }
        }

        private void AddPointer()
        {
            _cachedPointer = pointerPool.Obtain();
            _gridCell.AddChild(_cachedPointer.gameObject);
        }
        
        private void RemovePointer()
        {
            _gridCell.RemoveChild(_cachedPointer.gameObject);
            pointerPool.Reclaim(_cachedPointer);
        }
    }
}