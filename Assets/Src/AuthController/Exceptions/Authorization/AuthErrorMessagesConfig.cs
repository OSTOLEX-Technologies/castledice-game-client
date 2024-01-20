namespace Src.AuthController.Exceptions.Authorization
{
    public static class AuthErrorMessagesConfig
    {
        internal const string NetworkError = "Networking error occured. Please try again";
        internal const string CancellationError = "Login has been cancelled";
        internal const string FailureError = "Oops! Something went wrong...";
        internal const string UnhandledError = "Oops! Seems like some error occured. Try again later";
    }
}