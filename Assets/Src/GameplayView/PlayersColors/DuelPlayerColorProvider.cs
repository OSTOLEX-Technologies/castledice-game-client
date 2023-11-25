using castledice_game_logic;
using Src.GameplayPresenter;

namespace Src.GameplayView.PlayersColors
{
    public class DuelPlayerColorProvider : IPlayerColorProvider
    {
        private readonly IPlayerDataProvider _playerDataProvider;

        public DuelPlayerColorProvider(IPlayerDataProvider playerDataProvider)
        {
            _playerDataProvider = playerDataProvider;
        }

        public PlayerColor GetPlayerColor(Player player)
        {
            return _playerDataProvider.GetId() == player.Id ? PlayerColor.Blue : PlayerColor.Red;
        }
    }
}