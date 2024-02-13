using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace Src.GameplayView.DestroyedContent
{
    public interface IDestroyedContentView
    {
        void ShowDestroyedContent(Vector2Int position, Content content);
        void RemoveDestroyedContent(Vector2Int position, Content content);
    }
}