using System.Threading.Tasks;
using Src.GameplayView.PlayersColors;

namespace Src.GameplayView.ActionPointsGiving
{
    public class ActionPointsPopupDemonstrator : IActionPointsPopupDemonstrator
    {
        private readonly IActionPointsPopupsProvider _popupsProvider;
        private PlayerColor _currentColor;

        public ActionPointsPopupDemonstrator(IActionPointsPopupsProvider popupsProvider)
        {
            _popupsProvider = popupsProvider;
        }

        public void ShowActionPointsPopup(PlayerColor playersColor, int amount)
        {
            if (_currentColor != playersColor)
            {
                var previousPopup = _popupsProvider.GetPopup(_currentColor);
                previousPopup.Hide();
                _currentColor = playersColor;
            }
            var popup = _popupsProvider.GetPopup(playersColor);
            popup.SetAmount(amount);
        }
    }
}