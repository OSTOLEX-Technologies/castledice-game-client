using castledice_game_logic;
using Src.GameplayPresenter;

namespace Src.GameplayView.PlayersColors
{
    public class DuelPlayerColorProvider : IPlayerColorProvider
    {
        private readonly Player _localPlayer;

        public DuelPlayerColorProvider(Player localPlayer)
        {
            _localPlayer = localPlayer;
        }

        public PlayerColor GetPlayerColor(Player player)
        {
            return player == _localPlayer ? PlayerColor.Blue : PlayerColor.Red;
        }
    }
}