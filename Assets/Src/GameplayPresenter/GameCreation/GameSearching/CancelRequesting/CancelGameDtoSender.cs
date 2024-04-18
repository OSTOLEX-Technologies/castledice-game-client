using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.NetworkingModule;

namespace Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting
{
    public class CancelGameDtoSender : ICancelGameDtoSender
    {
        private readonly IMessageSender _messageSender;
        
        public CancelGameDtoSender(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }
        
        public void SendDto(CancelGameDTO dto)
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientToServerMessageType.CancelGame);
            message.AddCancelGameDTO(dto);
            _messageSender.Send(message);
        }
    }
}