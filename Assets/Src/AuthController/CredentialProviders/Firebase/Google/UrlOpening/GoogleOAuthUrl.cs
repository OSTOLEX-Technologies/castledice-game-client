using Src.AuthController.AuthKeys;
using UnityEngine;

namespace Src.AuthController.CredentialProviders.Firebase.Google.UrlOpening
{
    public class GoogleOAuthUrl : IGoogleOAuthUrl
    {
        public void Open()
        {
            Application.OpenURL($"https://accounts.google.com/o/oauth2/v2/auth?client_id=" +
                                $"{GoogleAuthConfig.ClientId}&redirect_uri=" +
                                $"{GoogleAuthConfig.RedirectUri}&response_type=code&scope=email");
        }
    }
}