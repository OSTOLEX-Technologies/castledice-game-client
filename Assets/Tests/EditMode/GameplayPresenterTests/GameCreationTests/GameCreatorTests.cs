using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.ConfigsData;
using castledice_game_data_logic.TurnSwitchConditions;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Configs;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.DecksListCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests
{
    public class GameCreatorTests
    {
        private static Player FirstPlayer = GetPlayer(1);
        private static Player SecondPlayer = GetPlayer(2);
        
        [Test]
        public void CreateGame_ShouldPassPlayersIdsListFromGameStartData_ToGivenPlayersListCreator()
        {
            var gameStartData = GetGameStartData();
            var playersListCreatorMock = GetPlayersListCreatorMock();
            var gameCreator = new GameCreatorBuilder
            {
                PlayersListCreator = playersListCreatorMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            playersListCreatorMock.Verify(p => p.GetPlayersList(gameStartData.PlayersIds), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldPassBoardDataAndPlayersListFromGameStartData_ToGivenBoardConfigCreator()
        {
            var gameStartData = GetGameStartData();
            var boardConfigCreatorMock = GetBoardConfigCreatorMock();
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
            var playersList = new List<Player>{ FirstPlayer, SecondPlayer };
            playersListCreatorMock.Setup(p => p.GetPlayersList(It.IsAny<List<int>>())).Returns(playersList);
            var boardConfigCreatorMock = GetBoardConfigCreatorMock();
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
            var placeablesConfigCreatorMock = GetPlaceablesConfigCreatorMock();
            var gameCreator = new GameCreatorBuilder
            {
                PlaceablesConfigCreator = placeablesConfigCreatorMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            placeablesConfigCreatorMock.Verify(p => p.GetPlaceablesConfig(gameStartData.PlaceablesConfigData), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldPassListOfPlayerDeckDataFromGameStartData_ToGivenDecksListCreator()
        {
            var gameStartData = GetGameStartData();
            var decksListCreatorMock = GetDecksListCreatorMock();
            var gameCreator = new GameCreatorBuilder
            {
                DecksListCreator = decksListCreatorMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            decksListCreatorMock.Verify(p => p.GetDecksList(gameStartData.Decks), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldCreateTurnsSwitchConditionsConfig_WithGivenCreator()
        {
            var gameStartData = GetGameStartData();
            var turnSwitchConditionsConfigCreatorMock = GetTurnSwitchConditionsConfigCreatorMock();
            var gameCreator = new GameCreatorBuilder
            {
                TurnSwitchConditionsConfigCreator = turnSwitchConditionsConfigCreatorMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            turnSwitchConditionsConfigCreatorMock.Verify(p => p.GetTurnSwitchConditionsConfig(gameStartData.TscConfigData), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldCreateGame_WithTurnSwitchConditionsConfigFromCreator()
        {
            var gameStartData = GetGameStartData();
            var turnSwitchConditionsConfig = GetTurnSwitchConditionsConfig();
            var turnSwitchConditionsConfigCreatorMock = new Mock<ITurnSwitchConditionsConfigCreator>();
            turnSwitchConditionsConfigCreatorMock.Setup(p => p.GetTurnSwitchConditionsConfig(It.IsAny<TscConfigData>())).Returns(turnSwitchConditionsConfig);
            var gameCreator = new GameCreatorBuilder
            {
                TurnSwitchConditionsConfigCreator = turnSwitchConditionsConfigCreatorMock.Object
            }.Build();    
            
            var game = gameCreator.CreateGame(gameStartData);
            
            Assert.AreEqual(turnSwitchConditionsConfig, game.TurnSwitchConditionsConfig);
        }

        private class GameCreatorBuilder
        {
            public IPlayersListCreator PlayersListCreator { get; set; } = GetPlayersListCreatorMock().Object;
            public IBoardConfigCreator BoardConfigCreator { get; set; } = GetBoardConfigCreatorMock().Object;
            public IPlaceablesConfigCreator PlaceablesConfigCreator { get; set; } = GetPlaceablesConfigCreatorMock().Object;
            public ITurnSwitchConditionsConfigCreator TurnSwitchConditionsConfigCreator { get; set; } = GetTurnSwitchConditionsConfigCreatorMock().Object;
            public IDecksListCreator DecksListCreator { get; set; } = GetDecksListCreatorMock().Object;
            
            public GameCreator Build()
            {
                return new GameCreator(PlayersListCreator, BoardConfigCreator, PlaceablesConfigCreator, TurnSwitchConditionsConfigCreator, DecksListCreator);
            }
        }
        
        private static Mock<IPlayersListCreator> GetPlayersListCreatorMock()
        {
            var mock = new Mock<IPlayersListCreator>();
            mock.Setup(p => p.GetPlayersList(It.IsAny<List<int>>())).Returns(new List<Player>{FirstPlayer, SecondPlayer});
            return mock;
        }
            
        private static Mock<IBoardConfigCreator> GetBoardConfigCreatorMock()
        {
            var mock = new Mock<IBoardConfigCreator>();
            mock.Setup(p => p.GetBoardConfig(It.IsAny<BoardData>(), It.IsAny<List<Player>>())).Returns(GetBoardConfig(FirstPlayer, SecondPlayer));
            return mock;
        }
            
        private static Mock<IPlaceablesConfigCreator> GetPlaceablesConfigCreatorMock()
        {
            var mock = new Mock<IPlaceablesConfigCreator>();
            mock.Setup(p => p.GetPlaceablesConfig(It.IsAny<PlaceablesConfigData>())).Returns(new PlaceablesConfig(new KnightConfig(1, 2)));
            return mock;
        }
            
        private static Mock<IDecksListCreator> GetDecksListCreatorMock()
        {
            var mock = new Mock<IDecksListCreator>();
            mock.Setup(p => p.GetDecksList(It.IsAny<List<PlayerDeckData>>())).Returns(new Mock<IDecksList>().Object);
            return mock;
        }
        
        private static Mock<ITurnSwitchConditionsConfigCreator> GetTurnSwitchConditionsConfigCreatorMock()
        {
            var mock = new Mock<ITurnSwitchConditionsConfigCreator>();
            mock.Setup(p => p.GetTurnSwitchConditionsConfig(It.IsAny<TscConfigData>())).Returns(GetTurnSwitchConditionsConfig());
            return mock;
        }
    }
}