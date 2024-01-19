using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.ContentVisuals;
using Src.GameplayView.ContentVisuals.ContentColor;
using Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation;
using static Tests.ObjectCreationUtility;
using Color = UnityEngine.Color;
using Random = UnityEngine.Random;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests.VisualsCreationTests.KnightVisualCreationTests
{
    public class KnightVisualCreatorTests
    {
        [Test]
        public void GetKnightVisual_ShouldReturnInstantiatedPrefab_FromConfig()
        {
            var prefab = GetKnightVisual();
            var instantiatedPrefab = GetKnightVisual();
            var prefabProviderMock = new Mock<IKnightVisualPrefabProvider>();
            prefabProviderMock.Setup(x => x.GetKnightVisualPrefab()).Returns(prefab);
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(x => x.Instantiate(prefab)).Returns(instantiatedPrefab);
            var knightVisualCreator = new KnightVisualCreatorBuilder
            {
                PrefabProvider = prefabProviderMock.Object,
                Instantiator = instantiatorMock.Object
            }.Build();

            var actualKnightVisual = knightVisualCreator.GetKnightVisual(GetKnight());
            
            Assert.AreSame(instantiatedPrefab, actualKnightVisual);
        }

        [Test]
        public void GetKnightVisual_ShouldSetKnightVisualColor_WithColorFromProvider()
        {
            var knight = GetKnight();
            var expectedColor = Random.ColorHSV();
            var knightVisualMock = new Mock<KnightVisual>();
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(x => x.Instantiate(It.IsAny<KnightVisual>())).Returns(knightVisualMock.Object);
            var playerContentColorProviderMock = new Mock<IPlayerContentColorProvider>();
            playerContentColorProviderMock.Setup(x => x.GetContentColor(knight.GetOwner())).Returns(expectedColor);
            var knightVisualCreator = new KnightVisualCreatorBuilder
            {
                Instantiator = instantiatorMock.Object,
                ColorProvider = playerContentColorProviderMock.Object
            }.Build();
            
            var actualKnightVisual = knightVisualCreator.GetKnightVisual(knight);

            knightVisualMock.Verify(x => x.SetColor(expectedColor), Times.Once);
        }
        
        private class KnightVisualCreatorBuilder
        {
            public IKnightVisualPrefabProvider PrefabProvider { get; set; }
            public IPlayerContentColorProvider ColorProvider { get; set; }
            public IInstantiator Instantiator { get; set; }
            
            public KnightVisualCreatorBuilder()
            {
                var colorProviderMock = new Mock<IPlayerContentColorProvider>();
                colorProviderMock.Setup(x => x.GetContentColor(It.IsAny<Player>())).Returns(Color.black);
                ColorProvider = colorProviderMock.Object;
                var prefabProviderMock = new Mock<IKnightVisualPrefabProvider>();
                prefabProviderMock.Setup(x => x.GetKnightVisualPrefab()).Returns(GetKnightVisual());
                PrefabProvider = prefabProviderMock.Object;
                var instantiatorMock = new Mock<IInstantiator>();
                instantiatorMock.Setup(x => x.Instantiate(It.IsAny<KnightVisual>())).Returns(GetKnightVisual());
                Instantiator = instantiatorMock.Object;
            }
            
            public KnightVisualCreator Build()
            {
                return new KnightVisualCreator(PrefabProvider, ColorProvider, Instantiator);
            }
        }
    }
}