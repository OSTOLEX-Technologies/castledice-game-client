using Src.GameplayView.CellsContent.ContentCreation;

namespace Src.GameplayView.ActionPointsGiving
{
    public interface IActionPointsPopupsProvider
    {
        IActionPointsPopup GetPopup(PlayerColor playerColor);
    }
}