using System.Collections.Generic;
using System.Linq;

namespace Src.Auth.DeepLinking.LinkResolver
{
    public readonly struct ResolvedDeepLink
    {
        public readonly string LinkName;
        private readonly Dictionary<string, string> _parameters;
        public IEnumerable<KeyValuePair<string, string>> Parameters => _parameters.AsEnumerable();

        public ResolvedDeepLink(string linkName, Dictionary<string, string> parameters)
        {
            LinkName = linkName;
            _parameters = parameters;
        }
    }
}