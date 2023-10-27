using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.Cells;
using Src.GameplayView.Grid;

namespace Tests.EditMode
{
    public class CellsViewGeneratorsFactoryTests
    {
        [Test]
        public void GetGenerator_ShouldReturnSquareCellGenerator3D_IfSquareCellTypeGiven()
        {
            var squareCellsGenerator3D = new SquareCellsGenerator3D(new Mock<ISquareCellsFactory>().Object, new Mock<IGrid>().Object);
            var factory = new CellsViewGeneratorsFactory(squareCellsGenerator3D);
            
            var generator = factory.GetGenerator(CellType.Square);
            
            Assert.AreSame(squareCellsGenerator3D, generator);
        }
    }
}