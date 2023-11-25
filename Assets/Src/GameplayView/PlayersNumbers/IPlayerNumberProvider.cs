using castledice_game_logic;

namespace Src.GameplayView.PlayersNumbers
{
    public interface IPlayerNumberProvider
    {
        int GetPlayerNumber(Player player);
    }
}