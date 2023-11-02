using System.Threading.Tasks;
using Src.GameplayView.CellsContent.ContentCreation;

namespace Src.GameplayView.ActionPointsGiving
{
    public class ActionPointsPopupDemonstrator : IActionPointsPopupDemonstrator
    {
        private readonly IActionPointsPopupsProvider _popupsProvider;
        private readonly int _popupDisappearTimeMilliseconds;

        public ActionPointsPopupDemonstrator(IActionPointsPopupsProvider popupsProvider, int popupDisappearTimeMilliseconds)
        {
            _popupsProvider = popupsProvider;
            _popupDisappearTimeMilliseconds = popupDisappearTimeMilliseconds;
        }

        public void ShowActionPointsPopup(PlayerColor playersColor, int amount)
        {
            var popup = _popupsProvider.GetPopup(playersColor);
            popup.SetAmount(amount);
            popup.Show();
            Task.Run(() => HidePopup(popup));
        }

        private async Task HidePopup(IActionPointsPopup popup)
        {
            await Task.Delay(_popupDisappearTimeMilliseconds);
            popup.Hide();
        }
    }
}