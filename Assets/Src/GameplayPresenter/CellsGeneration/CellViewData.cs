using System;

namespace Src.GameplayPresenter.CellsGeneration
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

        private bool Equals(CellViewData other)
        {
            return AssetId == other.AssetId && IsNull == other.IsNull;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CellViewData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AssetId, IsNull);
        }
    }
}