using Src.Auth.DeepLinking.LinkResolver.LinkFormatter;

namespace Src.Auth.DeepLinking.DeepLinkTextInjector
{
    public class DeepLinkTextInjector : IDeepLinkTextInjector
    {
        private readonly IDeepLinkFormatter _linkFormatter;

        public DeepLinkTextInjector(IDeepLinkFormatter linkFormatter)
        {
            _linkFormatter = linkFormatter;
        }

        public string InjectLink(string targetText, string link)
        {
            var formattedLink = _linkFormatter.FormatLink(link);

            return targetText.Replace(IDeepLinkTextInjector.DeeplinkInjectionMark, formattedLink);
        }
    }
}