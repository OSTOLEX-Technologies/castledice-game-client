using castledice_game_logic;

namespace Src.GameplayView.CellsContent.ContentCreation
{
    public interface IPlayerColorProvider
    {
        PlayerColor GetPlayerColor(Player player);
    }
}