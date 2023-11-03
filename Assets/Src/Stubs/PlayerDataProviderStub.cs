using System;
using Src.GameplayPresenter;

namespace Src.Stubs
{
    //This class MUST NOT be used in a production build.
    public class PlayerDataProviderStub : IPlayerDataProvider
    {
        private readonly int _id;

        public PlayerDataProviderStub()
        {
            var rnd = new System.Random(Guid.NewGuid().GetHashCode());
            var number = rnd.Next(0, 1000000000);
            #if UNITY_EDITOR
            number += 1;
            #endif
            _id = number;
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