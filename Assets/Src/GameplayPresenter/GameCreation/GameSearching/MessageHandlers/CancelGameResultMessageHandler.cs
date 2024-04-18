using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace Src.GameplayPresenter.GameCreation.GameSearching.MessageHandlers
{
    public static class CancelGameResultMessageHandler
    {
        private static ICancelGameResultDtoAccepter _dtoAccepter;
        
        public static void SetDtoAccepter(ICancelGameResultDtoAccepter dtoAccepter)
        {
            _dtoAccepter = dtoAccepter;
        }
        
        [MessageHandler((ushort)ServerToClientMessageType.CancelGame)]
        private static void HandleMessage(Message message)
        {
            var dto = message.GetCancelGameResultDTO();
            _dtoAccepter.AcceptCancelGameResultDto(dto);
        }
    }
}