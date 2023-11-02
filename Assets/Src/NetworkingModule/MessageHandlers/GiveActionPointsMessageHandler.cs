using casltedice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule.MessageHandlers
{
    public static class GiveActionPointsMessageHandler
    {
        private static IGiveActionPointsDTOAccepter _DTOAccepter;
        
        public static void SetAccepter(IGiveActionPointsDTOAccepter accepter)
        {
            _DTOAccepter = accepter;
        }
        
        [MessageHandler((ushort) ServerToClientMessageType.GiveActionPoints)]
        private static void HandleGiveActionPointsMessage(Message message)
        {
            _DTOAccepter.AcceptGiveActionPointsDTO(message.GetGiveActionPointsDTO());
        }
    }
}