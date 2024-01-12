namespace Src.AuthController.DeepLinking.LinkResolver.LinkFormatter
{
    public interface IDeepLinkFormatter
    {
        public string FormatLink(string link);
        public string GetLinkWithoutScheme(string link);
    }
}