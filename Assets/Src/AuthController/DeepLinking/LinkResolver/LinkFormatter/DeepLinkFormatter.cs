using System;
using Src.AuthController.DeepLinking.Config;

namespace Src.AuthController.DeepLinking.LinkResolver.LinkFormatter
{
    public class DeepLinkFormatter : IDeepLinkFormatter
    {
        public string FormatLink(string link)
        {
            return $"{DeepLinkConfig.GlobalSchemeName}{DeepLinkConfig.DefaultSchemeProtocolDivider}{link}";
        }

        public string GetLinkWithoutScheme(string link)
        {
            return link.Split(DeepLinkConfig.DefaultSchemeProtocolDivider, StringSplitOptions.RemoveEmptyEntries)[1];
        }
    }
}