using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule.MessageHandlers
{
    public static class SwitchTimerMessageHandler
    {
        private static ISwitchTimerDTOAccepter _DTOAccepter;
        
        public static void SetAccepter(ISwitchTimerDTOAccepter accepter)
        {
            _DTOAccepter = accepter;
        }

        [MessageHandler((ushort)ServerToClientMessageType.SwitchTimer)]
        private static void HandleSwitchTimerMessage(Message message)
        {
            //var dto = message.GetSwitchTimerDTO();
            //_DTOAccepter.AcceptSwitchTimerDTO(dto);
        }
    }
}