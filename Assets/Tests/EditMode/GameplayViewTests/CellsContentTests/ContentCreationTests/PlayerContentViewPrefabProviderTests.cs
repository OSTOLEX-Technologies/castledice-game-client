using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentCreation;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode
{
    public class PlayerContentViewPrefabProviderTests
    {
        [Test]
        public void GetKnightPrefab_ShouldReturnBlueKnight_IfPlayerColorIsBlue()
        {
            var colorProviderMock = new Mock<IPlayerColorProvider>();
            colorProviderMock.Setup(c => c.GetPlayerColor(It.IsAny<Player>())).Returns(PlayerColor.Blue);
            var configMock = new Mock<IPlayerContentViewPrefabsConfig>();
            var prefabProvider = new PlayerContentViewPrefabProvider(colorProviderMock.Object, configMock.Object);

            prefabProvider.GetKnightPrefab(GetPlayer());

            configMock.Verify(c => c.BlueKnightPrefab, Times.Once);
        }
        
        [Test]
        public void GetKnightPrefab_ShouldReturnRedKnight_IfPlayerColorIsRed()
        {
            var colorProviderMock = new Mock<IPlayerColorProvider>();
            colorProviderMock.Setup(c => c.GetPlayerColor(It.IsAny<Player>())).Returns(PlayerColor.Red);
            var configMock = new Mock<IPlayerContentViewPrefabsConfig>();
            var prefabProvider = new PlayerContentViewPrefabProvider(colorProviderMock.Object, configMock.Object);

            prefabProvider.GetKnightPrefab(GetPlayer());

            configMock.Verify(c => c.RedKnightPrefab, Times.Once);
        }
        
        [Test]
        public void GetCastlePrefab_ShouldReturnBlueCastle_IfPlayerColorIsBlue()
        {
            var colorProviderMock = new Mock<IPlayerColorProvider>();
            colorProviderMock.Setup(c => c.GetPlayerColor(It.IsAny<Player>())).Returns(PlayerColor.Blue);
            var configMock = new Mock<IPlayerContentViewPrefabsConfig>();
            var prefabProvider = new PlayerContentViewPrefabProvider(colorProviderMock.Object, configMock.Object);

            prefabProvider.GetCastlePrefab(GetPlayer());

            configMock.Verify(c => c.BlueCastlePrefab, Times.Once);
        }
        
        [Test]
        public void GetCastlePrefab_ShouldReturnRedCastle_IfPlayerColorIsRed()
        {
            var colorProviderMock = new Mock<IPlayerColorProvider>();
            colorProviderMock.Setup(c => c.GetPlayerColor(It.IsAny<Player>())).Returns(PlayerColor.Red);
            var configMock = new Mock<IPlayerContentViewPrefabsConfig>();
            var prefabProvider = new PlayerContentViewPrefabProvider(colorProviderMock.Object, configMock.Object);

            prefabProvider.GetCastlePrefab(GetPlayer());

            configMock.Verify(c => c.RedCastlePrefab, Times.Once);
        }
    }
}