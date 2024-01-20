namespace Src.AuthController.Exceptions.Authorization
{
    public static class AuthErrorMessagesConfig
    {
        internal const string NetworkError = "Networking error occured. Please try again";
        internal const string CancellationError = "Login has been cancelled";
        internal const string FailureError = "Something went wrong...";
        internal const string UnhandledError = "Seems like some error occured. Try again later";
    }
}