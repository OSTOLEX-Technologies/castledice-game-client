namespace Src.GameplayView.Grid.GridGeneration
{
    public interface IGridGenerator
    {
        void GenerateGrid(bool[,] cellsPresenceMatrix);
    }
}