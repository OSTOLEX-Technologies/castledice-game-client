using Src.GameplayPresenter;

namespace Tests.Manual
{
    public class PlayerDataProviderStub : IPlayerDataProvider
    {
        public string GetAccessToken()
        {
            return "token";
        }

        public int GetId()
        {
            return 1;
        }

        public bool IsAuthorized()
        {
            return true;
        }
    }
}