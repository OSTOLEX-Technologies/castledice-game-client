using System;
using castledice_game_logic.Time;

namespace Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators
{
    public interface IPlayerTimerCreator
    {
        IPlayerTimer GetPlayerTimer(TimeSpan timeSpan);
    }
}