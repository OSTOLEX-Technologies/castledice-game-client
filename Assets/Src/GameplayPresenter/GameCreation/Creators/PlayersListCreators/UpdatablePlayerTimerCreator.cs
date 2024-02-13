using System;
using castledice_game_logic.Time;
using Src.GameplayView.Updatables;
using Src.TimeManagement;

namespace Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators
{
    public class UpdatablePlayerTimerCreator : IPlayerTimerCreator
    {
        private readonly ITimeDeltaProvider _timeDeltaProvider;
        private readonly IUpdater _updater;
        
        public UpdatablePlayerTimerCreator(ITimeDeltaProvider timeDeltaProvider, IUpdater updater)
        {
            _timeDeltaProvider = timeDeltaProvider;
            _updater = updater;
        }
        
        /// <summary>
        /// This methods creates instance of UpdatablePlayerTimer class with given timeSpan, adds it to updater and returns it.
        /// You don't need to add it to updater by yourself.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IPlayerTimer GetPlayerTimer(TimeSpan timeSpan)
        {
            var playerTimer = new UpdatablePlayerTimer(timeSpan, _timeDeltaProvider);
            _updater.AddUpdatable(playerTimer);
            return playerTimer;
        }
    }
}