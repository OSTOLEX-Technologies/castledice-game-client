using System.Collections.Generic;
using castledice_game_logic;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public class CachingPlayerTimerViewProvider : IPlayerTimerViewProvider
    {
        private readonly IPlayerTimerViewCreator _playerTimerViewCreator;
        private readonly Dictionary<Player, IPlayerTimerView> _cachedViews = new();

        public CachingPlayerTimerViewProvider(IPlayerTimerViewCreator playerTimerViewCreator)
        {
            _playerTimerViewCreator = playerTimerViewCreator;
        }

        public IPlayerTimerView GetTimerViewForPlayer(Player player)
        {
            if (!_cachedViews.ContainsKey(player))
            {
                _cachedViews[player] = _playerTimerViewCreator.Create(player);
            }

            return _cachedViews[player];
        }
    }
}