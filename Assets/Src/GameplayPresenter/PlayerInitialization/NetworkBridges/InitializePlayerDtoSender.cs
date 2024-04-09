using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.NetworkingModule;

namespace Src.GameplayPresenter.PlayerInitialization.NetworkBridges
{
    public class InitializePlayerDtoSender : IInitializePlayerDtoSender
    {
        private readonly IMessageSender _messageSender;

        public InitializePlayerDtoSender(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public void SendDto(InitializePlayerDTO dto)
        {
            var message = Message.Create(MessageSendMode.Unreliable, ClientToServerMessageType.CancelGame);
            message.AddInitializePlayerDTO(dto);
            _messageSender.Send(message);
        }
    }
}