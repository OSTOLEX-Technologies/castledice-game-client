using System;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace Src.GameplayPresenter.PlayerInitialization.NetworkBridges
{
    public class PlayerInitializationResultMessageAccepter : IPlayerInitializationResultEventsEmitter
    {
        public void AcceptMessage(Message message)
        {
            var dto = message.GetPlayerInitializationResultDTO();
            if (dto.IsSuccessful)
            {
                InitializationSucceed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                InitializationFailed?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler InitializationSucceed;
        public event EventHandler InitializationFailed;
    }
}