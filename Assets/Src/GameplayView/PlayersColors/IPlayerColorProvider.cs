using castledice_game_logic;

namespace Src.GameplayView.PlayersColors
{
    public interface IPlayerColorProvider
    {
        PlayerColor GetPlayerColor(Player player);
    }
}