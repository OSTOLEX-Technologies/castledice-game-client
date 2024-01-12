using System;

namespace Src.AuthController.DeepLinking.LinkResolver.LinkFormatter
{
    public class DeepLinkFormatter : IDeepLinkFormatter
    {
        private static string SchemeProtocolDivider => "://";

        public string FormatLink(string link)
        {
            return $"{DeepLinkConfig.GlobalDeepLinkSchemeName}{SchemeProtocolDivider}{link}";
        }

        public string GetLinkWithoutScheme(string link)
        {
            return link.Split(SchemeProtocolDivider, StringSplitOptions.RemoveEmptyEntries)[1];
        }
    }
}