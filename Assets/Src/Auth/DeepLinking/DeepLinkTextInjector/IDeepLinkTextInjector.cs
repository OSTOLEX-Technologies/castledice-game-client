namespace Src.Auth.DeepLinking.DeepLinkTextInjector
{
    public interface IDeepLinkTextInjector
    {
        protected const string DeeplinkInjectionMark = "DEEPLINK";
        
        public string InjectLink(string targetText, string link);
    }
}