using System;

namespace Src.Auth
{
    public interface IAuthView
    {
        public void Login(AuthType authType);
        public void HideAuthUI();

        public void ShowSignInMessage(string signInMessage);

        public event Action<AuthType> AuthTypeChosen;
        public event Action AuthCompleted;
    }
}