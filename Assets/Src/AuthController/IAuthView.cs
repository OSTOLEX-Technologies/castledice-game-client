using System;
using System.Collections.Generic;

namespace Src.AuthController
{
    public interface IAuthView
    {
        public void ShowAuthResult();

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}