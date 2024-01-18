using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.ConfigsData;
using castledice_game_data_logic.TurnSwitchConditions;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects.Configs;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests
{
    public class GameCreatorTests
    {
        [Test]
        public void CreateGame_ShouldPassPlayersDataListFromGameStartData_ToGivenPlayersListCreator()
        {
            var gameStartData = GetGameStartData();
            var playersListCreatorMock = new Mock<IPlayersListCreator>();
            var gameCreator = new GameCreatorBuilder
            {
                PlayersListCreator = playersListCreatorMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            playersListCreatorMock.Verify(p => p.GetPlayersList(gameStartData.PlayersData), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldPassBoardDataFromGameStartData_ToGivenBoardConfigCreator()
        {
            var gameStartData = GetGameStartData();
            var boardConfigCreatorMock = new Mock<IBoardConfigCreator>();
            var gameCreator = new GameCreatorBuilder
            {
                BoardConfigCreator = boardConfigCreatorMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            boardConfigCreatorMock.Verify(p => p.GetBoardConfig(gameStartData.BoardData, It.IsAny<List<Player>>()), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldPassPlayersListFromCreator_ToGivenBoardConfigCreator()
        {
            var gameStartData = GetGameStartData();
            var playersListCreatorMock = new Mock<IPlayersListCreator>();
            var playersList = new List<Player>();
            playersListCreatorMock.Setup(p => p.GetPlayersList(It.IsAny<List<PlayerData>>())).Returns(playersList);
            var boardConfigCreatorMock = new Mock<IBoardConfigCreator>();
            var gameCreator = new GameCreatorBuilder
            {
                PlayersListCreator = playersListCreatorMock.Object,
                BoardConfigCreator = boardConfigCreatorMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            boardConfigCreatorMock.Verify(p => p.GetBoardConfig(It.IsAny<BoardData>(), playersList), Times.Once);
        }

        
        [Test]
        public void CreateGame_ShouldPassPlaceablesConfigDataFromGameStartData_ToGivenPlaceablesConfigCreator()
        {
            var gameStartData = GetGameStartData();
            var placeablesConfigCreatorMock = new Mock<IPlaceablesConfigCreator>();
            var gameCreator = new GameCreatorBuilder
            {
                PlaceablesConfigCreator = placeablesConfigCreatorMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            placeablesConfigCreatorMock.Verify(p => p.GetPlaceablesConfig(gameStartData.PlaceablesConfigData), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldCreateTurnsSwitchConditionsConfig_WithGivenCreator()
        {
            var gameStartData = GetGameStartData();
            var turnSwitchConditionsConfigCreatorMock = new Mock<ITurnSwitchConditionsConfigCreator>();
            var gameCreator = new GameCreatorBuilder
            {
                TurnSwitchConditionsConfigCreator = turnSwitchConditionsConfigCreatorMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            turnSwitchConditionsConfigCreatorMock.Verify(p => p.GetTurnSwitchConditionsConfig(gameStartData.TscConfigData), Times.Once); 
        }

        [Test]
        public void CreateGame_ShouldPassPlayersListFromCreator_ToGivenBuilder()
        {
            var playersList = GetPlayersList();
            var playersListCreatorMock = new Mock<IPlayersListCreator>();
            playersListCreatorMock.Setup(p => p.GetPlayersList(It.IsAny<List<PlayerData>>())).Returns(playersList);
            var builderMock = GetGameBuilderMock();
            var gameCreator = new GameCreatorBuilder
            {
                PlayersListCreator = playersListCreatorMock.Object,
                GameBuilder = builderMock.Object
            }.Build();
            
            gameCreator.CreateGame(GetGameStartData());
            
            builderMock.Verify(p => p.BuildPlayersList(playersList), Times.Once);
        }
        
        [Test]
        public void CreateGame_ShouldPassBoardConfigFromCreator_ToGivenBuilder()
        {
            var boardConfig = GetBoardConfig();
            var boardConfigCreatorMock = new Mock<IBoardConfigCreator>();
            boardConfigCreatorMock.Setup(p => p.GetBoardConfig(It.IsAny<BoardData>(), It.IsAny<List<Player>>())).Returns(boardConfig);
            var builderMock = GetGameBuilderMock();
            var gameCreator = new GameCreatorBuilder
            {
                BoardConfigCreator = boardConfigCreatorMock.Object,
                GameBuilder = builderMock.Object
            }.Build();
            
            gameCreator.CreateGame(GetGameStartData());
            
            builderMock.Verify(p => p.BuildBoardConfig(boardConfig), Times.Once);
        }
        
        [Test]
        public void CreateGame_ShouldPassPlaceablesConfigFromCreator_ToGivenBuilder()
        {
            var placeablesConfig = new PlaceablesConfig(new KnightConfig(1, 2));
            var placeablesConfigCreatorMock = new Mock<IPlaceablesConfigCreator>();
            placeablesConfigCreatorMock.Setup(p => p.GetPlaceablesConfig(It.IsAny<PlaceablesConfigData>())).Returns(placeablesConfig);
            var builderMock = GetGameBuilderMock();
            var gameCreator = new GameCreatorBuilder
            {
                PlaceablesConfigCreator = placeablesConfigCreatorMock.Object,
                GameBuilder = builderMock.Object
            }.Build();
            
            gameCreator.CreateGame(GetGameStartData());
            
            builderMock.Verify(p => p.BuildPlaceablesConfig(placeablesConfig), Times.Once);
        }
        
        [Test]
        public void CreateGame_ShouldPassTurnSwitchConditionsConfigFromCreator_ToGivenBuilder()
        {
            var turnSwitchConditionsConfig = GetTurnSwitchConditionsConfig();
            var turnSwitchConditionsConfigCreatorMock = new Mock<ITurnSwitchConditionsConfigCreator>();
            turnSwitchConditionsConfigCreatorMock.Setup(p => p.GetTurnSwitchConditionsConfig(It.IsAny<TscConfigData>())).Returns(turnSwitchConditionsConfig);
            var builderMock = GetGameBuilderMock();
            var gameCreator = new GameCreatorBuilder
            {
                TurnSwitchConditionsConfigCreator = turnSwitchConditionsConfigCreatorMock.Object,
                GameBuilder = builderMock.Object
            }.Build();
            
            gameCreator.CreateGame(GetGameStartData());
            
            builderMock.Verify(p => p.BuildTurnSwitchConditionsConfig(turnSwitchConditionsConfig), Times.Once);
        }
        
        [Test]
        public void CreateGame_ShouldReturnGame_FromBuilder()
        {
            var expectedGame = GetGame();
            var builderMock = GetGameBuilderMock();
            builderMock.Setup(p => p.Build()).Returns(expectedGame);
            var gameCreator = new GameCreatorBuilder
            {
                GameBuilder = builderMock.Object
            }.Build();
            
            var actualGame = gameCreator.CreateGame(GetGameStartData());
            
            Assert.AreSame(expectedGame, actualGame);
        }

        private class GameCreatorBuilder
        {
            public IPlayersListCreator PlayersListCreator { get; set; } = new Mock<IPlayersListCreator>().Object;
            public IBoardConfigCreator BoardConfigCreator { get; set; } = new Mock<IBoardConfigCreator>().Object;
            public IPlaceablesConfigCreator PlaceablesConfigCreator { get; set; } = new Mock<IPlaceablesConfigCreator>().Object;
            public ITurnSwitchConditionsConfigCreator TurnSwitchConditionsConfigCreator { get; set; } = new Mock<ITurnSwitchConditionsConfigCreator>().Object;
            public IGameBuilder GameBuilder { get; set; } = GetGameBuilderMock().Object;
            
            
            public GameCreator Build()
            {
                return new GameCreator(PlayersListCreator, BoardConfigCreator, PlaceablesConfigCreator, TurnSwitchConditionsConfigCreator, GameBuilder);
            }
        }

        private static Mock<IGameBuilder> GetGameBuilderMock()
        {
            var gameBuilderMock = new Mock<IGameBuilder>();
            gameBuilderMock.Setup(p => p.Build()).Returns(GetGame());
            gameBuilderMock.Setup(p => p.BuildPlayersList(It.IsAny<List<Player>>())).Returns(gameBuilderMock.Object);
            gameBuilderMock.Setup(p => p.BuildBoardConfig(It.IsAny<BoardConfig>())).Returns(gameBuilderMock.Object);
            gameBuilderMock.Setup(p => p.BuildPlaceablesConfig(It.IsAny<PlaceablesConfig>())).Returns(gameBuilderMock.Object);
            gameBuilderMock.Setup(p => p.BuildTurnSwitchConditionsConfig(It.IsAny<TurnSwitchConditionsConfig>())).Returns(gameBuilderMock.Object);
            return gameBuilderMock;
        }
    }
}