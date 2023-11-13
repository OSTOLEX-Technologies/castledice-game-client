using System;

namespace Src.GameplayPresenter.CellsGeneration.SquareCellsGeneration
{
    public sealed class SquareCellData
    {
        public VerticalOrientationType VerticalOrientationType { get; }
        public HorizontalOrientationType HorizontalOrientationType { get; }
        public SquareCellBorderType BorderType { get; }

        public SquareCellData(VerticalOrientationType verticalOrientationType, HorizontalOrientationType horizontalOrientationType, SquareCellBorderType borderType)
        {
            VerticalOrientationType = verticalOrientationType;
            BorderType = borderType;
            HorizontalOrientationType = horizontalOrientationType;
        }

        
        
        private bool Equals(SquareCellData other)
        {
            return VerticalOrientationType == other.VerticalOrientationType && HorizontalOrientationType == other.HorizontalOrientationType && BorderType == other.BorderType;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is SquareCellData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)VerticalOrientationType, (int)HorizontalOrientationType, (int)BorderType);
        }
    }
}