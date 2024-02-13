using System;

namespace Src.Auth
{
    public interface IAuthView
    {
        public void Login(AuthType authType);

        public void ShowSignInMessage(string signInMessage);

        public event EventHandler<AuthType> AuthTypeChosen;
        public event EventHandler AuthCompleted;
    }
}