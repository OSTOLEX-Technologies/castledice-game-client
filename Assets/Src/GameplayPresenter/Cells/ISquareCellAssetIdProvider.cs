using System.Collections.Generic;

namespace Src.GameplayPresenter.Cells
{
    public interface ISquareCellAssetIdProvider
    {
        List<int> GetAssetIds(SquareCellData data);
    }
}