using System;
using castledice_game_logic.GameObjects;

namespace Tests.Mocks
{
    public class ObstacleMock : Content, IPlaceBlocking
    {
        public bool IsBlocking()
        {
            return true;
        }

        public override void Update()
        {
        
        }

        public override T Accept<T>(IContentVisitor<T> visitor)
        {
            throw new NotImplementedException();
        }
    }
}