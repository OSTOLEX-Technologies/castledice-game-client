using Src.GameplayView.Grid;

namespace Src.Tutorial.HintPointer
{
    public interface IHintPointerPool
    {
        public HintPointer Obtain();

        public bool RemovePointerAtCell(IGridCell cell);
        
        public void Reclaim(HintPointer pointer);
    }
}