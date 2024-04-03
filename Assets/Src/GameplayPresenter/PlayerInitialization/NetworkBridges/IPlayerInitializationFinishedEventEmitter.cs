using System;
using castledice_events_logic.ServerToClient;

namespace Src.GameplayPresenter.PlayerInitialization.NetworkBridges
{
    public interface IPlayerInitializationFinishedEventEmitter
    {
        public event EventHandler<PlayerInitializationResultDTO> InitializationFinished;
    }
}