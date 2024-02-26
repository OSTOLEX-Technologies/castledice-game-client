using System;
using Src.Auth.DeepLinking.Config;

namespace Src.Auth.DeepLinking.LinkResolver.LinkFormatter
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