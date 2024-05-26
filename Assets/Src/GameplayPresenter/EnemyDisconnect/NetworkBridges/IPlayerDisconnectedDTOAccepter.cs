using castledice_events_logic.ServerToClient;

namespace Src.GameplayPresenter.EnemyDisconnect.NetworkBridges
{
    public interface IPlayerDisconnectedDTOAccepter
    {
        public void AcceptDTO(PlayerDisconnectedDTO dto);
    }
}