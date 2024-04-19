using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.NetworkingModule;

namespace Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting
{
    public class RequestGameDtoSender : IRequestGameDtoSender
    {
        private readonly IMessageSender _messageSender;
        
        public RequestGameDtoSender(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }
        
        public void SendDto(RequestGameDTO dto)
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientToServerMessageType.RequestGame);
            message.AddRequestGameDTO(dto);
            _messageSender.Send(message);
        }
    }
}