using System.Threading.Tasks;
using Src.GameplayView.PlayersColors;

namespace Src.GameplayView.ActionPointsGiving
{
    public class ActionPointsPopupDemonstrator : IActionPointsPopupDemonstrator
    {
        private readonly IActionPointsPopupsProvider _popupsProvider;

        public ActionPointsPopupDemonstrator(IActionPointsPopupsProvider popupsProvider)
        {
            _popupsProvider = popupsProvider;
        }

        public void ShowActionPointsPopup(PlayerColor playersColor, int amount)
        {
            var popup = _popupsProvider.GetPopup(playersColor);
            popup.Show();
            popup.SetAmount(amount);
        }
    }
}