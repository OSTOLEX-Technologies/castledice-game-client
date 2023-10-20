using System.Collections.Generic;

namespace Src.GameplayPresenter.Cells.SquareCellsGeneration
{
    public interface ISquareCellAssetIdProvider
    {
        List<int> GetAssetIds(SquareCellData data);
    }
}