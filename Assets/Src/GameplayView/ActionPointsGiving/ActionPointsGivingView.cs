using castledice_game_logic;
using Src.GameplayView.PlayersColors;

namespace Src.GameplayView.ActionPointsGiving
{
    public class ActionPointsGivingView : IActionPointsGivingView
    {
        private readonly IPlayerColorProvider _playerColorProvider;
        private readonly IActionPointsPopupDemonstrator _popupDemonstrator;

        public ActionPointsGivingView(IPlayerColorProvider playerColorProvider, IActionPointsPopupDemonstrator popupDemonstrator)
        {
            _playerColorProvider = playerColorProvider;
            _popupDemonstrator = popupDemonstrator;
        }

        public void ShowActionPointsForPlayer(Player player, int amount)
        {
            var playerColor = _playerColorProvider.GetPlayerColor(player);
            _popupDemonstrator.ShowActionPointsPopup(playerColor, amount);
        }
    }
}