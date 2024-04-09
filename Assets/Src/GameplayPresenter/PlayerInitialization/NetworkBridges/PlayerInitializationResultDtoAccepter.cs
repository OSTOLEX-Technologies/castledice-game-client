using System;
using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace Src.GameplayPresenter.PlayerInitialization.NetworkBridges
{
    public class PlayerInitializationResultDtoAccepter : IPlayerInitializationResultEventsEmitter
    {
        public void AcceptDto(PlayerInitializationResultDTO dto)
        {
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