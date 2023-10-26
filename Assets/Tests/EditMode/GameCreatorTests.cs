using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.Content.Placeable;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Configs;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode
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
                PlayersListProvider = playersListProviderMock.Object
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
            var playersListProviderMock = new Mock<IPlayersListProvider>();
            var playersList = new List<Player>{ FirstPlayer, SecondPlayer };
            playersListProviderMock.Setup(p => p.GetPlayersList(It.IsAny<List<int>>())).Returns(playersList);
            var boardConfigProviderMock = GetBoardConfigProviderMock();
            var gameCreator = new GameCreatorBuilder
            {
                PlayersListProvider = playersListProviderMock.Object,
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
                PlaceablesConfigProvider = placeablesConfigProviderMock.Object
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
                DecksListProvider = decksListProviderMock.Object
            }.Build();    
            
            gameCreator.CreateGame(gameStartData);
            
            decksListProviderMock.Verify(p => p.GetDecksList(gameStartData.Decks), Times.Once); 
        }

        private class GameCreatorBuilder
        {
            public IPlayersListProvider PlayersListProvider { get; set; } = GetPlayersListProviderMock().Object;
            public IBoardConfigProvider BoardConfigProvider { get; set; } = GetBoardConfigProviderMock().Object;
            public IPlaceablesConfigProvider PlaceablesConfigProvider { get; set; } = GetPlaceablesConfigProviderMock().Object;
            public IDecksListProvider DecksListProvider { get; set; } = GetDecksListProviderMock().Object;
            
            public GameCreator Build()
            {
                return new GameCreator(PlayersListProvider, BoardConfigProvider, PlaceablesConfigProvider, DecksListProvider);
            }
        }
        
        private static Mock<IPlayersListProvider> GetPlayersListProviderMock()
        {
            var mock = new Mock<IPlayersListProvider>();
            mock.Setup(p => p.GetPlayersList(It.IsAny<List<int>>())).Returns(new List<Player>{FirstPlayer, SecondPlayer});
            return mock;
        }
            
        private static Mock<IBoardConfigProvider> GetBoardConfigProviderMock()
        {
            var mock = new Mock<IBoardConfigProvider>();
            mock.Setup(p => p.GetBoardConfig(It.IsAny<BoardData>(), It.IsAny<List<Player>>())).Returns(GetBoardConfig(FirstPlayer, SecondPlayer));
            return mock;
        }
            
        private static Mock<IPlaceablesConfigProvider> GetPlaceablesConfigProviderMock()
        {
            var mock = new Mock<IPlaceablesConfigProvider>();
            mock.Setup(p => p.GetPlaceablesConfig(It.IsAny<PlaceablesConfigData>())).Returns(new PlaceablesConfig(new KnightConfig(1, 2)));
            return mock;
        }
            
        private static Mock<IDecksListProvider> GetDecksListProviderMock()
        {
            var mock = new Mock<IDecksListProvider>();
            mock.Setup(p => p.GetDecksList(It.IsAny<List<PlayerDeckData>>())).Returns(new Mock<IDecksList>().Object);
            return mock;
        }
    }
}