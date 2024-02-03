using Src.Auth.DeepLinking.Config;

namespace Src.Auth.AuthKeys
{
    public static class GoogleAuthConfig
    {
        internal const string ClientId = "150757942999-dku1lgumktq12lp4k1uoftt7cvrhiicc.apps.googleusercontent.com";

        internal const string ClientSecret = "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";
        internal const string Verifier = "ZRm7RHaD9Co4oBDXIi3ffHcgFG-q1-XZpiqkLcDATSA";
        
        internal const int LoopbackPort = 3033;
        internal static readonly string RedirectUri = $"http://127.0.0.1:{LoopbackPort}/";

        internal const string AuthCodeQueryKeyName = "code";

        public static readonly string GoogleOAuthUrl = "https://accounts.google.com/o/oauth2/v2/auth?" +
                                                       $"client_id={ClientId}&" +
                                                       $"redirect_uri={RedirectUri}&" +
                                                       $"response_type={AuthCodeQueryKeyName}&" +
                                                       "scope=email";
        
        internal static readonly string AuthRedirectDeepLink = $"{DeepLinkConfig.GlobalSchemeName}" +
                                                              $"{DeepLinkConfig.DefaultSchemeProtocolDivider}" +
                                                              $"{DeepLinkConfig.GoogleAuthRedirectUri}";

        
        internal static readonly string ResponseHtml = "<html>" +
                                                       "<head>" +
                                                       $"<meta http-equiv=\"Refresh\" content=\"0; url='{AuthRedirectDeepLink}'\">" +
                                                       "</head>" +
                                                       "<body>" +
                                                       "<h1>You could return to the app now.</h1>" +
                                                       "<p>If this page did not redirect you back into the game, press link below:</p>" +
                                                       $"<a href={AuthRedirectDeepLink}>| Click here |</a>" +
                                                       "</body>" +
                                                       "</html>";
    }
}