using castledice_game_data_logic;
using castledice_game_logic;

namespace Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators
{
    public class PlayerCreator : IPlayerCreator
    {
        private readonly IPlayerTimerCreator _playerTimerCreator;

        public PlayerCreator(IPlayerTimerCreator playerTimerCreator)
        {
            _playerTimerCreator = playerTimerCreator;
        }

        public Player GetPlayer(PlayerData playerData)
        {
            throw new System.NotImplementedException();
        }
    }
}