using System;
using castledice_game_logic.Time;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;

namespace Src.TimeManagement
{
    public class InfinityPlayerTimerCreator :IPlayerTimerCreator
    {
        public IPlayerTimer GetPlayerTimer(TimeSpan timeSpan)
        {
            return new InfinityPlayerTimer();
        }
    }
}