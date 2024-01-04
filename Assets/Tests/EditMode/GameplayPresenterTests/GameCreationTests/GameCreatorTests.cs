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
        public void CreateGame_ShouldPassPlayersIdsListFromGameStartData_ToGivenPlayersListProvider()
        {
            var gameStartData = GetGameStartData();
            var playersListProviderMock = GetPlayersListProviderMock();
            var gameCreator = new GameCreatorBuilder
            {
                PlayersListCreator = playersListProviderMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            playersListProviderMock.Verify(p => p.GetPlayersList(gameStartData.PlayersIds), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldPassBoardDataAndPlayersListFromGameStartData_ToGivenBoardConfigProvider()
        {
            var gameStartData = GetGameStartData();
            var boardConfigProviderMock = GetBoardConfigProviderMock();
            var gameCreator = new GameCreatorBuilder
            {
                BoardConfigProvider = boardConfigProviderMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            boardConfigProviderMock.Verify(p => p.GetBoardConfig(gameStartData.BoardData, It.IsAny<List<Player>>()), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldPassPlayersListFromProvider_ToGivenBoardConfigProvider()
        {
            var gameStartData = GetGameStartData();
            var playersListProviderMock = new Mock<IPlayersListCreator>();
            var playersList = new List<Player>{ FirstPlayer, SecondPlayer };
            playersListProviderMock.Setup(p => p.GetPlayersList(It.IsAny<List<int>>())).Returns(playersList);
            var boardConfigProviderMock = GetBoardConfigProviderMock();
            var gameCreator = new GameCreatorBuilder
            {
                PlayersListCreator = playersListProviderMock.Object,
                BoardConfigProvider = boardConfigProviderMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            boardConfigProviderMock.Verify(p => p.GetBoardConfig(It.IsAny<BoardData>(), playersList), Times.Once);
        }

        
        [Test]
        public void CreateGame_ShouldPassPlaceablesConfigDataFromGameStartData_ToGivenPlaceablesConfigProvider()
        {
            var gameStartData = GetGameStartData();
            var placeablesConfigProviderMock = GetPlaceablesConfigProviderMock();
            var gameCreator = new GameCreatorBuilder
            {
                PlaceablesConfigCreator = placeablesConfigProviderMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            placeablesConfigProviderMock.Verify(p => p.GetPlaceablesConfig(gameStartData.PlaceablesConfigData), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldPassListOfPlayerDeckDataFromGameStartData_ToGivenDecksListProvider()
        {
            var gameStartData = GetGameStartData();
            var decksListProviderMock = GetDecksListProviderMock();
            var gameCreator = new GameCreatorBuilder
            {
                DecksListCreator = decksListProviderMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            decksListProviderMock.Verify(p => p.GetDecksList(gameStartData.Decks), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldCreateTurnsSwitchConditionsConfig_WithGivenProvider()
        {
            var gameStartData = GetGameStartData();
            var turnSwitchConditionsConfigProviderMock = GetTurnSwitchConditionsConfigProviderMock();
            var gameCreator = new GameCreatorBuilder
            {
                TurnSwitchConditionsConfigProvider = turnSwitchConditionsConfigProviderMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            turnSwitchConditionsConfigProviderMock.Verify(p => p.GetTurnSwitchConditionsConfig(gameStartData.TscConfigData), Times.Once); 
        }
        
        [Test]
        public void CreateGame_ShouldCreateGame_WithTurnSwitchConditionsConfigFromProvider()
        {
            var gameStartData = GetGameStartData();
            var turnSwitchConditionsConfig = GetTurnSwitchConditionsConfig();
            var turnSwitchConditionsConfigProviderMock = new Mock<ITurnSwitchConditionsConfigProvider>();
            turnSwitchConditionsConfigProviderMock.Setup(p => p.GetTurnSwitchConditionsConfig(It.IsAny<TscConfigData>())).Returns(turnSwitchConditionsConfig);
            var gameCreator = new GameCreatorBuilder
            {
                TurnSwitchConditionsConfigProvider = turnSwitchConditionsConfigProviderMock.Object
            }.Build();    
            
            var game = gameCreator.CreateGame(gameStartData);
            
            Assert.AreEqual(turnSwitchConditionsConfig, game.TurnSwitchConditionsConfig);
        }

        private class GameCreatorBuilder
        {
            public IPlayersListCreator PlayersListCreator { get; set; } = GetPlayersListProviderMock().Object;
            public IBoardConfigProvider BoardConfigProvider { get; set; } = GetBoardConfigProviderMock().Object;
            public IPlaceablesConfigCreator PlaceablesConfigCreator { get; set; } = GetPlaceablesConfigProviderMock().Object;
            public ITurnSwitchConditionsConfigProvider TurnSwitchConditionsConfigProvider { get; set; } = GetTurnSwitchConditionsConfigProviderMock().Object;
            public IDecksListCreator DecksListCreator { get; set; } = GetDecksListProviderMock().Object;
            
            public GameCreator Build()
            {
                return new GameCreator(PlayersListCreator, BoardConfigProvider, PlaceablesConfigCreator, TurnSwitchConditionsConfigProvider, DecksListCreator);
            }
        }
        
        private static Mock<IPlayersListCreator> GetPlayersListProviderMock()
        {
            var mock = new Mock<IPlayersListCreator>();
            mock.Setup(p => p.GetPlayersList(It.IsAny<List<int>>())).Returns(new List<Player>{FirstPlayer, SecondPlayer});
            return mock;
        }
            
        private static Mock<IBoardConfigProvider> GetBoardConfigProviderMock()
        {
            var mock = new Mock<IBoardConfigProvider>();
            mock.Setup(p => p.GetBoardConfig(It.IsAny<BoardData>(), It.IsAny<List<Player>>())).Returns(GetBoardConfig(FirstPlayer, SecondPlayer));
            return mock;
        }
            
        private static Mock<IPlaceablesConfigCreator> GetPlaceablesConfigProviderMock()
        {
            var mock = new Mock<IPlaceablesConfigCreator>();
            mock.Setup(p => p.GetPlaceablesConfig(It.IsAny<PlaceablesConfigData>())).Returns(new PlaceablesConfig(new KnightConfig(1, 2)));
            return mock;
        }
            
        private static Mock<IDecksListCreator> GetDecksListProviderMock()
        {
            var mock = new Mock<IDecksListCreator>();
            mock.Setup(p => p.GetDecksList(It.IsAny<List<PlayerDeckData>>())).Returns(new Mock<IDecksList>().Object);
            return mock;
        }
        
        private static Mock<ITurnSwitchConditionsConfigProvider> GetTurnSwitchConditionsConfigProviderMock()
        {
            var mock = new Mock<ITurnSwitchConditionsConfigProvider>();
            mock.Setup(p => p.GetTurnSwitchConditionsConfig(It.IsAny<TscConfigData>())).Returns(GetTurnSwitchConditionsConfig());
            return mock;
        }
    }
}