using castledice_game_logic;

namespace Src.GameplayPresenter.GameWrappers
{
    public interface IPlayerProvider
    {
        Player GetPlayer(int playerId);
    }
}