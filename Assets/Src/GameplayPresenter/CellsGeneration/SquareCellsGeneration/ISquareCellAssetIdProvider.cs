using System.Collections.Generic;

namespace Src.GameplayPresenter.CellsGeneration.SquareCellsGeneration
{
    public interface ISquareCellAssetIdProvider
    {
        List<int> GetAssetIds(SquareCellData data);
    }
}