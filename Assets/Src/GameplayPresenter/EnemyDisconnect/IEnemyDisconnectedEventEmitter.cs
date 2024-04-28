using System;

namespace Src.GameplayPresenter.EnemyDisconnect
{
    public interface IEnemyDisconnectedEventEmitter
    {
        public event Action EnemyDisconnected;
    }
}