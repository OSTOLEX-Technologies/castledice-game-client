using castledice_game_data_logic;
using Moq;
using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayPresenter.GameCreation;
using Src.Tutorial;

namespace Tests.EditMode.TutorialTests
{
    public class TutorialGameCreatorTests
    {
        [Test]
        public void CreateGame_ShouldPassGivenPlayerAndEnemyId_ToGameStartDataProvider()
        {
            var rnd = new System.Random();
            var playerId = rnd.Next();
            var enemyId = rnd.Next();
            var gameCreator = new Mock<IGameCreator>().Object;
            var gameStartDataProviderMock = new Mock<ITutorialGameStartDataProvider>();
            var tutorialGameCreator = new TutorialGameCreator(gameCreator, gameStartDataProviderMock.Object, playerId, enemyId);
            
            tutorialGameCreator.CreateGame();
            
            gameStartDataProviderMock.Verify(x => x.GetGameStartData(playerId, enemyId), Times.Once);
        }
        
        [Test]
        public void CreateGame_ShouldCallGameStartDataProvider_Once()
        {
            var gameCreator = new Mock<IGameCreator>().Object;
            var gameStartDataProviderMock = new Mock<ITutorialGameStartDataProvider>();
            var tutorialGameCreator = new TutorialGameCreator(gameCreator, gameStartDataProviderMock.Object, 0, 1);
            
            tutorialGameCreator.CreateGame();
            
            gameStartDataProviderMock.Verify(x => x.GetGameStartData(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CreateGame_ShouldPassDataFromProvider_ToCreator()
        {
            var gameStartData = GetGameStartData();
            var gameCreatorMock = new Mock<IGameCreator>();
            var gameStartDataProvider = new Mock<ITutorialGameStartDataProvider>();
            gameStartDataProvider.Setup(x => x.GetGameStartData(It.IsAny<int>(), It.IsAny<int>())).Returns(gameStartData);
            var tutorialGameCreator = new TutorialGameCreator(gameCreatorMock.Object, gameStartDataProvider.Object, 0, 1);
            
            tutorialGameCreator.CreateGame();
            
            gameCreatorMock.Verify(x => x.CreateGame(gameStartData), Times.Once);
        }
        
        [Test]
        public void CreateGame_ShouldCallGameCreator_Once()
        {
            var gameCreatorMock = new Mock<IGameCreator>();
            var gameStartDataProvider = new Mock<ITutorialGameStartDataProvider>();
            var tutorialGameCreator = new TutorialGameCreator(gameCreatorMock.Object, gameStartDataProvider.Object, 0, 1);
            
            tutorialGameCreator.CreateGame();
            
            gameCreatorMock.Verify(x => x.CreateGame(It.IsAny<GameStartData>()), Times.Once);
        }

        [Test]
        public void CreateGame_ShouldReturnGame_FromCreator()
        {
            var expectedGame = GetGame();
            var gameCreatorMock = new Mock<IGameCreator>();
            gameCreatorMock.Setup(x => x.CreateGame(It.IsAny<GameStartData>())).Returns(expectedGame);
            var gameStartDataProvider = new Mock<ITutorialGameStartDataProvider>();
            var tutorialGameCreator = new TutorialGameCreator(gameCreatorMock.Object, gameStartDataProvider.Object, 0, 1);
            
            var game = tutorialGameCreator.CreateGame();
            
            Assert.AreSame(expectedGame, game);
        }
    }
}