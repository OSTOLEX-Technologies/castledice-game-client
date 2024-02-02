using System;

namespace Src.Auth.Exceptions.Authorization
{
    public interface IGoogleAuthExceptionCreator
    {
        public Exception FormatAuthException(Exception caughtException);
    }
}