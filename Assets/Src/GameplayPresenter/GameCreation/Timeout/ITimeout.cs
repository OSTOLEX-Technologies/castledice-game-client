using System;

namespace Src.GameplayPresenter.GameCreation.Timeout
{
    public interface ITimeout
    {
        public void StartCountdown();
        public void CancelCountdown();
        
        public event Action TimeOut;
    }
}