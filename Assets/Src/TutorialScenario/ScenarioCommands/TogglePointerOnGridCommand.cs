using Src.GameplayView.Grid;
using Src.Tutorial.HintPointer;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class TogglePointerOnGridCommand : ITutorialScenarioCommand
    {
        private readonly IGridCell _cell;
        private readonly IHintPointerPool _pointerPool;
        private readonly bool _enabled;
        private HintPointer _cachedPointer;

        public TogglePointerOnGridCommand(
            IGridCell cell, 
            IHintPointerPool pointerPool,
            bool enabled)
        {
            _cell = cell;
            _pointerPool = pointerPool;
            _enabled = enabled;
        }
        
        public void Do()
        {
            if (_enabled)
            {
                AddPointer();
            }
            else
            {
                RemovePointer();
            }
        }

        public void Undo()
        {
            if (_enabled)
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
            _cachedPointer = _pointerPool.Obtain();
            _cell.AddChild(_cachedPointer.gameObject);
        }
        
        private void RemovePointer()
        {
            _cell.RemoveChild(_cachedPointer.gameObject);
            _pointerPool.Reclaim(_cachedPointer);
        }
    }
}