using System.Collections.Generic;

namespace Src.Auth.DeepLinking.LinkResolver
{
    public struct ResolvedDeepLink
    {
        public readonly string LinkName;
        public Dictionary<string, string> Parameters;

        public ResolvedDeepLink(string linkName, Dictionary<string, string> parameters)
        {
            LinkName = linkName;
            Parameters = parameters;
        }
    }
}