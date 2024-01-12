using System.Collections.Generic;

namespace Src.AuthController.DeepLinking.LinkResolver
{
    public struct SResolvedDeepLink
    {
        public string LinkName;
        public Dictionary<string, string> Parameters;

        public SResolvedDeepLink(string linkName, Dictionary<string, string> parameters)
        {
            LinkName = linkName;
            Parameters = parameters;
        }
    }
}