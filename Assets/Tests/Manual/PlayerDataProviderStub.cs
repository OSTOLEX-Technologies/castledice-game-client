using Src.GameplayPresenter;

namespace Tests.Manual
{
    public class PlayerDataProviderStub : IPlayerDataProvider
    {
        public string GetAccessTokenAsync()
        {
            return "token";
        }

        public int GetIdAsync()
        {
            return 1;
        }

        public bool IsAuthorized()
        {
            return true;
        }
    }
}