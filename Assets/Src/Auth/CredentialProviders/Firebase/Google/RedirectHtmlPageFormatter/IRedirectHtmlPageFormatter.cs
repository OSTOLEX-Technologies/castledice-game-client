namespace Src.Auth.CredentialProviders.Firebase.Google.RedirectHtmlPageFormatter
{
    public interface IRedirectHtmlPageFormatter
    {
        public string StreamingAssetFileName { get; }
        public string FormatPage();
    }
}