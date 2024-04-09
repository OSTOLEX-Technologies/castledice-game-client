using System;
using castledice_events_logic.ServerToClient;
using Riptide;

namespace Src.GameplayPresenter.PlayerInitialization.NetworkBridges
{
    public class PlayerInitializationResultMessageHandler : IPlayerInitializationResultEventsEmitter
    {
        private static void HandleMessage(Message message)
        {
            
        }

        public event EventHandler InitializationSucceed;
        public event EventHandler InitializationFailed;
    }
}