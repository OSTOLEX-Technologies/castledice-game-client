using System;
using castledice_game_logic.Time;

namespace Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators
{
    public class StopwatchPlayerTimerCreator : IPlayerTimerCreator
    {
        public IPlayerTimer GetPlayerTimer(TimeSpan timeSpan)
        {
            return new StopwatchPlayerTimer(timeSpan);
        }
    }
}