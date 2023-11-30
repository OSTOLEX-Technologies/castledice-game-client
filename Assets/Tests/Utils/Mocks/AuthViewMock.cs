using System;
using Src.AuthController;

namespace Tests.Utils.Mocks
{
    public class AuthViewMock : IAuthView
    {
        public void ShowSignInResult()
        {
            throw new NotImplementedException();
        }

        public void SelectAuthType(AuthType authType)
        {
            AuthTypeChosen?.Invoke(this, authType);
        }

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}