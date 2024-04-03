using castledice_events_logic.ClientToServer;
using Src.NetworkingModule;

namespace Src.GameplayPresenter.PlayerInitialization.NetworkBridges
{
    public class InitializePlayerDtoSender : IInitializePlayerDtoSender
    {
        private readonly IMessageSender _messageSender;
        
        public void SendDto(InitializePlayerDTO dto)
        {
            
        }
    }
}