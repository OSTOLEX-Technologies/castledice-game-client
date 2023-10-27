﻿using System.Collections.Generic;
using castledice_game_data_logic.Content.Generated;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;
using static Tests.ObjectCreationUtility;


namespace Tests.EditMode
{
    public class BoardConfigProviderTests
    {
        public static CellType[] CellTypes = {CellType.Square, CellType.Triangle};
        
        [Test]
        public void GetBoardConfig_ShouldReturnBoardConfig_WithSpawnersListFromGivenProvider()
        {
            var spawnersProviderMock = new Mock<IContentSpawnersListProvider>();
            var cellsGeneratorProviderMock = new Mock<ICellsGeneratorProvider>();
            var expectedList = new List<IContentSpawner>();
            spawnersProviderMock.Setup(provider => provider.GetContentSpawnersList(It.IsAny<List<GeneratedContentData>>(), It.IsAny<List<Player>>()))
                .Returns(expectedList);
            var boardConfigProvider = new BoardConfigProvider(spawnersProviderMock.Object, cellsGeneratorProviderMock.Object);
            
            var boardConfig = boardConfigProvider.GetBoardConfig(GetBoardData(), new List<Player>());
            
            Assert.AreSame(expectedList, boardConfig.ContentSpawners);
        }
        
        [Test]
        public void GetBoardConfig_ShouldReturnBoardConfig_WithCellsGeneratorFromGivenProvider()
        {
            var spawnersProviderMock = new Mock<IContentSpawnersListProvider>();
            var cellsGeneratorProviderMock = new Mock<ICellsGeneratorProvider>();
            var expectedGenerator = new Mock<ICellsGenerator>().Object;
            cellsGeneratorProviderMock.Setup(provider => provider.GetCellsGenerator(It.IsAny<bool[,]>()))
                .Returns(expectedGenerator);
            var boardConfigProvider = new BoardConfigProvider(spawnersProviderMock.Object, cellsGeneratorProviderMock.Object);
            
            var boardConfig = boardConfigProvider.GetBoardConfig(GetBoardData(), new List<Player>());
            
            Assert.AreSame(expectedGenerator, boardConfig.CellsGenerator);
        }

        [Test]
        public void GetBoardConfig_ShouldReturnBoardConfig_WithCellTypeFromGivenBoardData([ValueSource(nameof(CellTypes))] CellType cellType)
        {
            var spawnersProviderMock = new Mock<IContentSpawnersListProvider>();
            var cellsGeneratorProviderMock = new Mock<ICellsGeneratorProvider>();
            var boardConfigProvider = new BoardConfigProvider(spawnersProviderMock.Object, cellsGeneratorProviderMock.Object);
            var boardData = GetBoardData(cellType);
            
            var boardConfig = boardConfigProvider.GetBoardConfig(boardData, new List<Player>());
            
            Assert.AreEqual(cellType, boardConfig.CellType);
        }

        [Test]
        public void GetBoardConfig_ShouldPassGeneratedContentDataListFromBoardData_ToGivenContentSpawnersListProvider()
        {
            var boardData = GetBoardData();
            var spawnersProviderMock = new Mock<IContentSpawnersListProvider>();
            var cellsGeneratorProviderMock = new Mock<ICellsGeneratorProvider>();
            var boardConfigProvider = new BoardConfigProvider(spawnersProviderMock.Object, cellsGeneratorProviderMock.Object);
            
            boardConfigProvider.GetBoardConfig(boardData, new List<Player>());
            
            spawnersProviderMock.Verify(provider => provider.GetContentSpawnersList(boardData.GeneratedContent, It.IsAny<List<Player>>()), Times.Once);
        }

        [Test]
        public void GetBoardConfig_ShouldPassGivenPlayersList_ToGivenContentSpawnersListProvider()
        {
            var players = new List<Player>();
            var spawnersProviderMock = new Mock<IContentSpawnersListProvider>();
            var cellsGeneratorProviderMock = new Mock<ICellsGeneratorProvider>();
            var boardConfigProvider = new BoardConfigProvider(spawnersProviderMock.Object, cellsGeneratorProviderMock.Object);
            
            boardConfigProvider.GetBoardConfig(GetBoardData(), players);
            
            spawnersProviderMock.Verify(provider => provider.GetContentSpawnersList(It.IsAny<List<GeneratedContentData>>(), players), Times.Once);
        }
        
        [Test]
        public void GetBoardConfig_ShouldPassCellsPresenceFromBoardData_ToGivenCellsGeneratorProvider()
        {
            var boardData = GetBoardData();
            var spawnersProviderMock = new Mock<IContentSpawnersListProvider>();
            var cellsGeneratorProviderMock = new Mock<ICellsGeneratorProvider>();
            var boardConfigProvider = new BoardConfigProvider(spawnersProviderMock.Object, cellsGeneratorProviderMock.Object);
            
            boardConfigProvider.GetBoardConfig(boardData, new List<Player>());
            
            cellsGeneratorProviderMock.Verify(provider => provider.GetCellsGenerator(boardData.CellsPresence), Times.Once);
        }
    }
}