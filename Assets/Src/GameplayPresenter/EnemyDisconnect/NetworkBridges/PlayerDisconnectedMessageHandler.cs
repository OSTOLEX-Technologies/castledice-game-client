using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace Src.GameplayPresenter.EnemyDisconnect.NetworkBridges
{
    public static class PlayerDisconnectedMessageHandler
    {
        private static IPlayerDisconnectedDTOAccepter _dtoAccepter;
        
        public static void SetDTOAccepter(IPlayerDisconnectedDTOAccepter dtoAccepter)
        {
            _dtoAccepter = dtoAccepter;
        }
        
        [MessageHandler((ushort)ServerToClientMessageType.PlayerDisconnected)]
        private static void HandleMessage(Message message)
        {
            _dtoAccepter.AcceptDTO(message.GetPlayerDisconnectedDTO());
        }
    }
}