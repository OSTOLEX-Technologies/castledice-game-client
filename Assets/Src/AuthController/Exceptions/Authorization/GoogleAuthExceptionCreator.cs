using System;
using Newtonsoft.Json;
using Src.AuthController.Exceptions.HttpRequests;

namespace Src.AuthController.Exceptions.Authorization
{
    public class GoogleAuthExceptionCreator : IGoogleAuthExceptionCreator
    {
        private const string AuthInvalidRequestErrorName = "invalid_request";

        public Exception FormatAuthException(Exception caughtException) {
            switch (caughtException)
            {
                case HttpNetworkNotReachableException:
                    return caughtException;
                case HttpClientRequestException:
                    var errorData = JsonConvert.DeserializeObject<GoogleAuthErrorResponseDto>(caughtException.Message);
                    if (errorData == null) return new AuthUnhandledException(caughtException.Message);
                    return errorData.Error.Equals(AuthInvalidRequestErrorName)
                        ? new AuthFailedException(errorData.Description, errorData.Error)
                        : new AuthCancelledException();
                default: return new AuthUnhandledException();
            }
        }
    }
}