using casltedice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule.MessageHandlers
{
    public static class ApproveMoveMessageHandler
    {
        private static IApproveMoveDTOAccepter _DTOAccepter;
        
        public static void SetDTOAccepter(IApproveMoveDTOAccepter accepter)
        {
            _DTOAccepter = accepter;
        }
        
        [MessageHandler((ushort) ServerToClientMessageType.ApproveMove)]
        private static void HandleApproveMoveMessage(Message message)
        {
            _DTOAccepter.AcceptApproveMoveDTO(message.GetApproveMoveDTO());
        }
    }
}