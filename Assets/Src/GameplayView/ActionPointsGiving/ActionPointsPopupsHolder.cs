using Src.GameplayView.PlayersColors;

namespace Src.GameplayView.ActionPointsGiving
{
    /// <summary>
    /// This class is needed to hold and provide access to popups that are placed in the scene.
    /// </summary>
    public class ActionPointsPopupsHolder : IActionPointsPopupsProvider
    {
        private readonly IActionPointsPopup _bluePopup;
        private readonly IActionPointsPopup _redPopup;

        public ActionPointsPopupsHolder(IActionPointsPopup bluePopup, IActionPointsPopup redPopup)
        {
            _bluePopup = bluePopup;
            _redPopup = redPopup;
        }

        public IActionPointsPopup GetPopup(PlayerColor playerColor)
        {
            return playerColor == PlayerColor.Blue ? _bluePopup : _redPopup;
        }
    }
}