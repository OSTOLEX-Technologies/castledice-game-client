namespace Src.GameplayView.Grid
{
    public interface IGridGenerator
    {
        void GenerateGrid(bool[,] cellsPresenceMatrix);
    }
}