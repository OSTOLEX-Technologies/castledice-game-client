using Src.GameplayView.CellsContent.ContentCreation;

namespace Src.GameplayView.ActionPointsGiving
{
    public interface IActionPointsPopupDemonstrator
    {
        void ShowActionPointsPopup(PlayerColor playersColor, int amount);
    }
}