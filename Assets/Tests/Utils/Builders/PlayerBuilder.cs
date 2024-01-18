using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Time;
using Moq;

namespace Tests.Builders
{
    public class PlayerBuilder
    {
        public int Id { get; set; } = 1;
        public List<PlacementType> AvailablePlacements { get; set; } = new List<PlacementType>();
        public IPlayerTimer PlayerTimer { get; set; } = new Mock<IPlayerTimer>().Object;
        public PlayerActionPoints PlayerActionPoints { get; set; } = new Mock<PlayerActionPoints>().Object;
        
        public Player Build()
        {
            return new Player(PlayerActionPoints, PlayerTimer,  AvailablePlacements, Id);
        }
    }
}