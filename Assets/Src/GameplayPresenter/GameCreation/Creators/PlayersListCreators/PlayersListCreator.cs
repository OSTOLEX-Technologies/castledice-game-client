using System.Collections.Generic;
using System.Linq;
using castledice_game_data_logic;
using castledice_game_logic;

namespace Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators
{
    public class PlayersListCreator : IPlayersListCreator
    {
        private readonly IPlayerCreator _playerCreator;

        public PlayersListCreator(IPlayerCreator playerCreator)
        {
            _playerCreator = playerCreator;
        }

        public List<Player> GetPlayersList(List<PlayerData> playersData)
        {
            return playersData.Select(playerData => _playerCreator.GetPlayer(playerData)).ToList();
        }
    }
}