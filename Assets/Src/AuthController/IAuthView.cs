using System;

namespace Src.AuthController
{
    public interface IAuthView
    {
        public void Login(AuthType authType);

        public void ShowSignInMessage(bool bShow, string signInMessage);

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}