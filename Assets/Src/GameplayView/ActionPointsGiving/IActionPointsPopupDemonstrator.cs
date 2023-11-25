using Src.GameplayView.CellsContent.ContentCreation;
using Src.GameplayView.PlayersColors;

namespace Src.GameplayView.ActionPointsGiving
{
    public interface IActionPointsPopupDemonstrator
    {
        void ShowActionPointsPopup(PlayerColor playersColor, int amount);
    }
}