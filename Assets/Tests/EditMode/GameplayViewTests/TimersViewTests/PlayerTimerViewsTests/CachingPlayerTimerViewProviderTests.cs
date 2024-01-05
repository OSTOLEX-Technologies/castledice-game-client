using Moq;
using NUnit.Framework;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.Timers.PlayerTimerViews;

namespace Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests
{
    public class CachingPlayerTimerViewProviderTests
    {
        [Test]
        public void GetTimerViewForPlayer_ShouldReturnPlayerTimerViewFromCreator()
        {
            var player = GetPlayer();
            var expectedPlayerTimerView = new Mock<IPlayerTimerView>().Object;
            var playerTimerViewCreatorMock = new Mock<IPlayerTimerViewCreator>();
            playerTimerViewCreatorMock.Setup(creator => creator.Create(player))
                .Returns(expectedPlayerTimerView);
            var playerTimerViewProvider = new CachingPlayerTimerViewProvider(playerTimerViewCreatorMock.Object);
            
            var actualPlayerTimerView = playerTimerViewProvider.GetTimerViewForPlayer(player);
            
            Assert.AreEqual(expectedPlayerTimerView, actualPlayerTimerView);
        }
        
        [Test]
        public void GetTimerViewForPlayer_ShouldReturnSamePlayerTimerView_ForSamePlayer()
        {
            var player = GetPlayer();
            var expectedPlayerTimerView = new Mock<IPlayerTimerView>().Object;
            var playerTimerViewCreatorMock = new Mock<IPlayerTimerViewCreator>();
            playerTimerViewCreatorMock.Setup(creator => creator.Create(player))
                .Returns(expectedPlayerTimerView);
            var playerTimerViewProvider = new CachingPlayerTimerViewProvider(playerTimerViewCreatorMock.Object);
            
            var firstPlayerTimerView = playerTimerViewProvider.GetTimerViewForPlayer(player);
            var secondPlayerTimerView = playerTimerViewProvider.GetTimerViewForPlayer(player);
            
            Assert.AreSame(firstPlayerTimerView, secondPlayerTimerView);
        }
    }
}