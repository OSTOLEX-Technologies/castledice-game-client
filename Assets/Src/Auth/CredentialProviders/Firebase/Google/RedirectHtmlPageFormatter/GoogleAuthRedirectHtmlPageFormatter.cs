using UnityEngine;

namespace Src.Auth.CredentialProviders.Firebase.Google.RedirectHtmlPageFormatter
{
    public sealed class GoogleAuthRedirectHtmlPageFormatter : RedirectHtmlPageFormatterBase, IRedirectHtmlPageFormatter
    {
        public string StreamingAssetFileName => $"{StreamingAssetsFolderName}/GoogleAuthRedirectPage";
        
        public string FormatPage()
        {
            return Resources.Load<TextAsset>(StreamingAssetFileName).text;
        }
    }
}