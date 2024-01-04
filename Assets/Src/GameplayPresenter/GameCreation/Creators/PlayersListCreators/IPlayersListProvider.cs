using System.Collections.Generic;
using castledice_game_logic;

namespace Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators
{
    public interface IPlayersListProvider
    {
        List<Player> GetPlayersList(List<int> ids);
    }
}