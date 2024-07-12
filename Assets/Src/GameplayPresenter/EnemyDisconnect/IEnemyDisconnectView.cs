using System;

namespace Src.GameplayPresenter.EnemyDisconnect
{
    public interface IEnemyDisconnectView
    {
        public void ShowPopup();
        public event Action OkPressed;
    }
}