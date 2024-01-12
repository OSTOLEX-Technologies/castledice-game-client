using System.Collections.Generic;

namespace Src.AuthController.DeepLinking.LinkResolver.ParametersExtractor
{
    public interface IDeepLinkDetailsExtractor
    {
        public Dictionary<string, string> TryGetParameters(string link);

        public string GetLinkName(string link);
    }
}