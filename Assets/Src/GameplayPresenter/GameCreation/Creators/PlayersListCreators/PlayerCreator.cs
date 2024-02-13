using castledice_game_data_logic;
using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;

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
            var timer = _playerTimerCreator.GetPlayerTimer(playerData.TimeSpan);
            var actionPoints = new PlayerActionPoints();
            return new Player(actionPoints, timer, playerData.AvailablePlacements, playerData.PlayerId);
        }
    }
}