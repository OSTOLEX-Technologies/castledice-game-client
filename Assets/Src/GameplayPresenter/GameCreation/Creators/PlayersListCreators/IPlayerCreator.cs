using castledice_game_data_logic;
using castledice_game_logic;

namespace Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators
{
    public interface IPlayerCreator
    {
        Player GetPlayer(PlayerData playerData);
    }
}