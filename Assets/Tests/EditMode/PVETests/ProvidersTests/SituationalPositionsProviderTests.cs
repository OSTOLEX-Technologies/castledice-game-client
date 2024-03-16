using System.Collections.Generic;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.PVE.GameSituations;
using Src.PVE.Providers;

namespace Tests.EditMode.PVETests.ProvidersTests
{
    public class SituationalPositionsProviderTests
    {
        [Test]
        public void GetPositions_ShouldReturnEmptyList_IfDictionaryIsEmpty()
        {
            var dictionary = new Dictionary<IGameSituation, List<Vector2Int>>();
            var situationalPositionsProvider = new SituationalPositionsProvider(dictionary);
            
            var result = situationalPositionsProvider.GetPositions();
            
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetPositions_ShouldReturnEmptyList_IfNoSituationsOccured()
        {
            var situationMock = new Mock<IGameSituation>();
            situationMock.Setup(s => s.IsSituation()).Returns(false);
            var dictionary = new Dictionary<IGameSituation, List<Vector2Int>> {{situationMock.Object, new List<Vector2Int>()}};
            var situationalPositionsProvider = new SituationalPositionsProvider(dictionary);
            
            var result = situationalPositionsProvider.GetPositions();
            
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetPositions_ShouldReturnList_ForFirstOccuredSituation()
        {
            var firstOccuredSituation = new Mock<IGameSituation>();
            var secondOccuredSituation = new Mock<IGameSituation>();
            firstOccuredSituation.Setup(s => s.IsSituation()).Returns(true);
            secondOccuredSituation.Setup(s => s.IsSituation()).Returns(true);
            var expectedPositions = new List<Vector2Int> {};
            var dictionary = new Dictionary<IGameSituation, List<Vector2Int>>
            {
                {new Mock<IGameSituation>().Object, new List<Vector2Int>()},
                {new Mock<IGameSituation>().Object, new List<Vector2Int>()},
                {firstOccuredSituation.Object, expectedPositions},
                {new Mock<IGameSituation>().Object, new List<Vector2Int>()},
                {secondOccuredSituation.Object, new List<Vector2Int>()},
            };
            var situationalPositionsProvider = new SituationalPositionsProvider(dictionary);
            
            var result = situationalPositionsProvider.GetPositions();
            
            Assert.AreSame(expectedPositions, result);
        }
    }
}