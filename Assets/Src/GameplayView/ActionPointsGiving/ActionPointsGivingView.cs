using castledice_game_logic;
using Src.GameplayView.CellsContent.ContentCreation;

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
            throw new System.NotImplementedException();
        }
    }
}