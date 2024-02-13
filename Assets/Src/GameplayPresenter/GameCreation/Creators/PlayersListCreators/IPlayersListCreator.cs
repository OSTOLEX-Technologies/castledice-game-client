using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_logic;

namespace Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators
{
    public interface IPlayersListCreator
    {
        List<Player> GetPlayersList(List<PlayerData> playersData);
    }
}