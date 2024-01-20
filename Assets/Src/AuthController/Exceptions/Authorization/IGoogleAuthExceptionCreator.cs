using System;

namespace Src.AuthController.Exceptions.Authorization
{
    public interface IGoogleAuthExceptionCreator
    {
        public Exception FormatAuthException(Exception caughtException);
    }
}