using UnityEngine;

namespace Src.GameplayView.Cells
{
    public interface ISquareCellsFactory
    {
        GameObject GetSquareCell(int assetId);
    }
}