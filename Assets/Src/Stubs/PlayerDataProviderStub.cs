using Src.GameplayPresenter;

namespace Src.Stubs
{
    //This class MUST NOT be used in a production build.
    public class PlayerDataProviderStub : IPlayerDataProvider
    {
        private int _id;

        public PlayerDataProviderStub()
        {
            var rnd = new System.Random();
            _id = rnd.Next(0, 10000000);
        }

        public string GetAccessToken()
        {
            return _id.ToString();
        }

        public int GetId()
        {
            return _id;
        }

        public bool IsAuthorized()
        {
            return true;
        }
    }
}