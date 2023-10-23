using Src.GameplayView.CellsContent.ContentViews;

namespace Src.GameplayView.CellsContent.ContentCreation
{
    public interface IPlayerContentViewPrefabsConfig
    {
        KnightView RedKnightPrefab { get; }
        KnightView BlueKnightPrefab { get; }
        CastleView RedCastlePrefab { get; }
        CastleView BlueCastlePrefab { get; }
    }
}