using System.Collections.Generic;

namespace Src.Auth.DeepLinking.LinkResolver.ParametersExtractor
{
    public interface IDeepLinkDetailsExtractor
    {
        public Dictionary<string, string> TryGetParameters(string link);

        public string GetLinkName(string link);
    }
}