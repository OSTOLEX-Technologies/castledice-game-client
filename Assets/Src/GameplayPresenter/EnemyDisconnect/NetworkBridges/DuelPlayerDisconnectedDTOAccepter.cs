using System;
using castledice_events_logic.ServerToClient;

namespace Src.GameplayPresenter.EnemyDisconnect.NetworkBridges
{
    public class DuelPlayerDisconnectedDTOAccepter : IEnemyDisconnectedEventEmitter, IPlayerDisconnectedDTOAccepter
    {
        public event Action EnemyDisconnected;
        public void AcceptDTO(PlayerDisconnectedDTO dto)
        {
            EnemyDisconnected?.Invoke();
        }
    }
}