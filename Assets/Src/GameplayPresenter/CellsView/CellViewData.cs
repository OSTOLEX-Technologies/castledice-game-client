namespace Src.GameplayPresenter.CellsView
{
    public sealed class CellViewData
    {
        public int AssetId { get; }
        public bool IsNull { get; }

        public CellViewData(int assetId, bool isNull)
        {
            AssetId = assetId;
            IsNull = isNull;
        }
    }
}