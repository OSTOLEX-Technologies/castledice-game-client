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
            throw new System.NotImplementedException();
        }

        private async Task HidePopup()
        {
            
        }
    }
}