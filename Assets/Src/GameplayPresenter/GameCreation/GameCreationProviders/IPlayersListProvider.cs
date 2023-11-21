using System.Collections.Generic;
using castledice_game_logic;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public interface IPlayersListProvider
    {
        List<Player> GetPlayersList(List<int> ids);
    }
}