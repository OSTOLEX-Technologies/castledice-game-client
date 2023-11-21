using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public class PlayersListProvider : IPlayersListProvider
    {
        public List<Player> GetPlayersList(List<int> ids)
        {
            var playersList = new List<Player>();
            foreach (var id in ids)
            {
                playersList.Add(new Player(new PlayerActionPoints(),id));
            }

            return playersList;
        }
    }
}