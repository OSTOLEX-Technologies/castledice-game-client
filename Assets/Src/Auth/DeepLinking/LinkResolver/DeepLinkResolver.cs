using Src.Auth.DeepLinking.LinkResolver.LinkFormatter;
using Src.Auth.DeepLinking.LinkResolver.ParametersExtractor;

namespace Src.Auth.DeepLinking.LinkResolver
{
    public class DeepLinkResolver
    {
        private readonly IDeepLinkFormatter _linkFormatter;
        private readonly IDeepLinkDetailsExtractor _detailsExtractor;

        public DeepLinkResolver(IDeepLinkFormatter linkFormatter, IDeepLinkDetailsExtractor detailsExtractor)
        {
            _linkFormatter = linkFormatter;
            _detailsExtractor = detailsExtractor;
        }

        public SResolvedDeepLink TryResolveLink(string link)
        {
            var linkWithoutScheme = _linkFormatter.GetLinkWithoutScheme(link);
            return new SResolvedDeepLink(_detailsExtractor.GetLinkName(linkWithoutScheme), _detailsExtractor.TryGetParameters(linkWithoutScheme));
        }
    }
}