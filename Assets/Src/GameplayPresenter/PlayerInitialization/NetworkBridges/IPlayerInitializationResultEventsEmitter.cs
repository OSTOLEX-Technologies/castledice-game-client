using System;
using castledice_events_logic.ServerToClient;

namespace Src.GameplayPresenter.PlayerInitialization.NetworkBridges
{
    public interface IPlayerInitializationResultEventsEmitter
    {
        public event EventHandler InitializationSucceed;
        public event EventHandler InitializationFailed;
    }
}