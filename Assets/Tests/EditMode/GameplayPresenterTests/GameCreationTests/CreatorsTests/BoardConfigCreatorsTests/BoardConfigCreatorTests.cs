using System.Collections.Generic;
using castledice_game_data_logic.Content;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;
using static Tests.Utils.ObjectCreationUtility;


namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreatorsTests.BoardConfigCreatorsTests
{
    public class BoardConfigCreatorTests
    {
        public static CellType[] CellTypes = {CellType.Square, CellType.Triangle};
        
        [Test]
        public void GetBoardConfig_ShouldReturnBoardConfig_WithSpawnersListFromGivenCreator()
        {
            var spawnersCreatorMock = new Mock<IContentSpawnersListCreator>();
            var cellsGeneratorCreatorMock = new Mock<ICellsGeneratorCreator>();
            var expectedList = new List<IContentSpawner>();
            spawnersCreatorMock.Setup(creator => creator.GetContentSpawnersList(It.IsAny<List<ContentData>>(), It.IsAny<List<Player>>()))
                .Returns(expectedList);
            var boardConfigCreator = new BoardConfigCreator(spawnersCreatorMock.Object, cellsGeneratorCreatorMock.Object);
            
            var boardConfig = boardConfigCreator.GetBoardConfig(GetBoardData(), new List<Player>());
            
            Assert.AreSame(expectedList, boardConfig.ContentSpawners);
        }
        
        [Test]
        public void GetBoardConfig_ShouldReturnBoardConfig_WithCellsGeneratorFromGivenCreator()
        {
            var spawnersCreatorMock = new Mock<IContentSpawnersListCreator>();
            var cellsGeneratorCreatorMock = new Mock<ICellsGeneratorCreator>();
            var expectedGenerator = new Mock<ICellsGenerator>().Object;
            cellsGeneratorCreatorMock.Setup(creator => creator.GetCellsGenerator(It.IsAny<bool[,]>()))
                .Returns(expectedGenerator);
            var boardConfigCreator = new BoardConfigCreator(spawnersCreatorMock.Object, cellsGeneratorCreatorMock.Object);
            
            var boardConfig = boardConfigCreator.GetBoardConfig(GetBoardData(), new List<Player>());
            
            Assert.AreSame(expectedGenerator, boardConfig.CellsGenerator);
        }

        [Test]
        public void GetBoardConfig_ShouldReturnBoardConfig_WithCellTypeFromGivenBoardData([ValueSource(nameof(CellTypes))] CellType cellType)
        {
            var spawnersCreatorMock = new Mock<IContentSpawnersListCreator>();
            var cellsGeneratorCreatorMock = new Mock<ICellsGeneratorCreator>();
            var boardConfigCreator = new BoardConfigCreator(spawnersCreatorMock.Object, cellsGeneratorCreatorMock.Object);
            var boardData = GetBoardData(cellType);
            
            var boardConfig = boardConfigCreator.GetBoardConfig(boardData, new List<Player>());
            
            Assert.AreEqual(cellType, boardConfig.CellType);
        }

        [Test]
        public void GetBoardConfig_ShouldPassContentDataListFromBoardData_ToGivenContentSpawnersListCreator()
        {
            var boardData = GetBoardData();
            var spawnersCreatorMock = new Mock<IContentSpawnersListCreator>();
            var cellsGeneratorCreatorMock = new Mock<ICellsGeneratorCreator>();
            var boardConfigCreator = new BoardConfigCreator(spawnersCreatorMock.Object, cellsGeneratorCreatorMock.Object);
            
            boardConfigCreator.GetBoardConfig(boardData, new List<Player>());
            
            spawnersCreatorMock.Verify(creator => creator.GetContentSpawnersList(boardData.GeneratedContent, It.IsAny<List<Player>>()), Times.Once);
        }

        [Test]
        public void GetBoardConfig_ShouldPassGivenPlayersList_ToGivenContentSpawnersListCreator()
        {
            var players = new List<Player>();
            var spawnersCreatorMock = new Mock<IContentSpawnersListCreator>();
            var cellsGeneratorCreatorMock = new Mock<ICellsGeneratorCreator>();
            var boardConfigCreator = new BoardConfigCreator(spawnersCreatorMock.Object, cellsGeneratorCreatorMock.Object);
            
            boardConfigCreator.GetBoardConfig(GetBoardData(), players);
            
            spawnersCreatorMock.Verify(creator => creator.GetContentSpawnersList(It.IsAny<List<ContentData>>(), players), Times.Once);
        }
        
        [Test]
        public void GetBoardConfig_ShouldPassCellsPresenceFromBoardData_ToGivenCellsGeneratorCreator()
        {
            var boardData = GetBoardData();
            var spawnersCreatorMock = new Mock<IContentSpawnersListCreator>();
            var cellsGeneratorCreatorMock = new Mock<ICellsGeneratorCreator>();
            var boardConfigCreator = new BoardConfigCreator(spawnersCreatorMock.Object, cellsGeneratorCreatorMock.Object);
            
            boardConfigCreator.GetBoardConfig(boardData, new List<Player>());
            
            cellsGeneratorCreatorMock.Verify(creator => creator.GetCellsGenerator(boardData.CellsPresence), Times.Once);
        }
    }
}