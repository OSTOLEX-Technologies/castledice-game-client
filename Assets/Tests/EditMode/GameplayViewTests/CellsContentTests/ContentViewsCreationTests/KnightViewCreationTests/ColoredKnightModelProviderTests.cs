using castledice_game_logic;
using castledice_game_logic.GameObjects;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsCreationTests.KnightViewCreationTests
{
    public class ColoredKnightModelProviderTests
    {
        [Test]
        public void GetKnightModel_ShouldReturnInstantiatedKnightModelPrefab()
        {
            var prefab = new GameObject();
            var instantiatedPrefab = new GameObject();
            var prefabProviderMock = new Mock<IKnightModelPrefabProvider>();
            var instantiatorMock = new Mock<IInstantiator>();
            prefabProviderMock.Setup(provider => provider.GetKnightModelPrefab(It.IsAny<PlayerColor>()))
                .Returns(prefab);
            instantiatorMock.Setup(instantiator => instantiator.Instantiate(prefab)).Returns(instantiatedPrefab);
            var knightModelProvider = new ColoredKnightModelProviderBuilder
            {
                Instantiator = instantiatorMock.Object,
                PrefabProvider = prefabProviderMock.Object
            }.Build();
            var knight = GetKnight();
            
            var knightModel = knightModelProvider.GetKnightModel(knight);
            
            Assert.AreSame(instantiatedPrefab, knightModel);
        }
        
        [Test]
        [TestCase(PlayerColor.Red)]
        [TestCase(PlayerColor.Blue)]
        public void GetKnight_ShouldRetrievePrefabFromProvider_AccordingToColor(PlayerColor color)
        {
            var prefab = new GameObject();
            var knight = GetKnight();
            var prefabProviderMock = new Mock<IKnightModelPrefabProvider>();
            prefabProviderMock.Setup(provider => provider.GetKnightModelPrefab(color)).Returns(prefab);
            var colorProviderMock = new Mock<IPlayerColorProvider>();
            colorProviderMock.Setup(provider => provider.GetPlayerColor(knight.GetOwner())).Returns(color);
            var knightModelProvider = new ColoredKnightModelProviderBuilder
            {
                PrefabProvider = prefabProviderMock.Object,
                ColorProvider = colorProviderMock.Object
            }.Build();
            
            var model = knightModelProvider.GetKnightModel(knight);
            
            prefabProviderMock.Verify(provider => provider.GetKnightModelPrefab(color), Times.Once);
        }

        private class ColoredKnightModelProviderBuilder
        {
            public IPlayerColorProvider ColorProvider { get; set; }
            public IKnightModelPrefabProvider PrefabProvider { get; set; }
            public IInstantiator Instantiator { get; set; }

            public ColoredKnightModelProviderBuilder()
            {
                var colorProviderMock = new Mock<IPlayerColorProvider>();
                colorProviderMock.Setup(provider => provider.GetPlayerColor(It.IsAny<Player>())).Returns(PlayerColor.Red);
                ColorProvider = colorProviderMock.Object;
                var prefab = new GameObject();
                var prefabProviderMock = new Mock<IKnightModelPrefabProvider>();
                prefabProviderMock.Setup(provider => provider.GetKnightModelPrefab(It.IsAny<PlayerColor>()))
                    .Returns(prefab);
                PrefabProvider = prefabProviderMock.Object;
                var instantiatedPrefab = new GameObject();
                var instantiatorMock = new Mock<IInstantiator>();
                instantiatorMock.Setup(instantiator => instantiator.Instantiate(prefab)).Returns(instantiatedPrefab);
                Instantiator = instantiatorMock.Object;
            }
            
            public ColoredKnightModelProvider Build()
            {
                return new ColoredKnightModelProvider(ColorProvider, PrefabProvider, Instantiator);
            }
        }
    }
}