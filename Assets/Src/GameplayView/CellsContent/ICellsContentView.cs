using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace Src.GameplayView.CellsContent
{
    public interface ICellsContentView
    {
        void AddViewForContent(Vector2Int position, Content content);
        void RemoveViewForContent(Content content);
        void UpdateViewForContent(Content content);
    }
}