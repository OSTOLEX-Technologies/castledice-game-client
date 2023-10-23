using UnityEngine;

namespace Src.GameplayView.Grid.GridGeneration
{
    public interface ISquareGridGenerationConfig
    {
        float CellLength { get; }
        float CellWidth { get; }
        Vector3 StartPosition { get; }
    }
}