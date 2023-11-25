using Src.GameplayView.PlayersColors;

namespace Src.GameplayView.ActionPointsGiving
{
    public interface IActionPointsPopupsProvider
    {
        IActionPointsPopup GetPopup(PlayerColor playerColor);
    }
}