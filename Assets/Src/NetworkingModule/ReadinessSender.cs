using casltedice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace Src.NetworkingModule
{
    public class ReadinessSender
    {
        private readonly IMessageSender _messageSender;
        
        public ReadinessSender(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public void SendPlayerReadiness(string verificationKey)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerMessageType.PlayerReady);
            var DTO = new PlayerReadyDTO(verificationKey);
            message.AddPlayerReadyDTO(DTO);
            _messageSender.Send(message);
        }
    }
}