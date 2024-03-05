namespace Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver
{
    public interface IStringSaver
    {
        public bool TryGetStringValue(string name, out string value);
        public void SaveStringValue(string name, string value);
    }
}