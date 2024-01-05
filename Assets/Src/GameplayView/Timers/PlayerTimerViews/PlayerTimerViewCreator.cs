using castledice_game_logic;
using Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public class PlayerTimerViewCreator : IPlayerTimerViewCreator
    {
        private readonly IPlayerHighlighterProvider _playerHighlighterProvider;
        private readonly IPlayerTimeViewProvider _playerTimeViewProvider;
        
        public PlayerTimerViewCreator(IPlayerHighlighterProvider playerHighlighterProvider, IPlayerTimeViewProvider playerTimeViewProvider)
        {
            _playerHighlighterProvider = playerHighlighterProvider;
            _playerTimeViewProvider = playerTimeViewProvider;
        }
        
        public IPlayerTimerView Create(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}