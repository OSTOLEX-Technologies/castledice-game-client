using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;

namespace Tests.EditMode.GameplayViewTests.GridTests.GridGenerationTests
{
    public class GridGeneratorsFactoryTests
    {
        [Test]
        public void GetGridGenerator_ShouldReturnGivenUnitySquareGridGenerator_IfCellTypeIsSquare()
        {
            var squareGridGenerator = new SquareGridGenerator(new Mock<IGrid>().Object, new Mock<ISquareGridGenerationConfig>().Object);
            var factory = new GridGeneratorsFactory(squareGridGenerator);
            
            var generator = factory.GetGridGenerator(CellType.Square);
            
            Assert.AreSame(squareGridGenerator, generator);
        }
    }
}