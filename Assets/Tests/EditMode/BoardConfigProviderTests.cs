using System.Collections.Generic;
using castledice_game_data_logic.Content.Generated;
using castledice_game_data_logic.Content.Placeable;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;
using Src.GameplayView.Cells;
using static Tests.ObjectCreationUtility;


namespace Tests.EditMode
{
    public class BoardConfigProviderTests
    {
        [Test]
        public void GetBoardConfig_ShouldReturnBoardConfig_WithSpawnersListFromGivenProvider()
        {
            var spawnersProvider = new Mock<IContentSpawnersListProvider>();
            var cellsGeneratorProvider = new Mock<ICellsGeneratorProvider>();
            var expectedList = new List<IContentSpawner>();
            spawnersProvider.Setup(provider => provider.GetContentSpawnersList(It.IsAny<List<GeneratedContentData>>(), It.IsAny<List<Player>>()))
                .Returns(expectedList);
            var boardConfigProvider = new BoardConfigProvider(spawnersProvider.Object, cellsGeneratorProvider.Object);
            
            var boardConfig = boardConfigProvider.GetBoardConfig(GetBoardData(), new List<Player>());
            
            Assert.AreSame(expectedList, boardConfig.ContentSpawners);
        }
        
        /*public void GetBoardConfig_ShouldReturnBoardConfig_WithCellsGeneratorFromGivenProvider()
        {
            var spawnersProvider = new Mock<IContentSpawnersListProvider>();
            var cellsGeneratorProvider = new Mock<ICellsGeneratorProvider>();
            var expectedGenerator = new Mock<ICellsGenerator>().Object;
            cellsGeneratorProvider.Setup(provider => provider.GetCellsGenerator(It.IsAny<bool[,]>()))
                .Returns(expectedGenerator);
            var boardConfigProvider = new BoardConfigProvider(spawnersProvider.Object, cellsGeneratorProvider.Object);
            
            var boardConfig = boardConfigProvider.GetBoardConfig(GetBoardData(), new List<Player>());
            
            Assert.AreSame(expectedGenerator.Object, boardConfig.CellsGenerator);
        }*/
    }
}