using System;

namespace Src.AuthController
{
    public interface IAuthView
    {
        public void ShowSignInResult();

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}