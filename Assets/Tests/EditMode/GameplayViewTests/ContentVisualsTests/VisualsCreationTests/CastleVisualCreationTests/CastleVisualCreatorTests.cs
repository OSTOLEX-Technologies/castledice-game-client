using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.ContentVisuals;
using Src.GameplayView.ContentVisuals.ContentColor;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests.VisualsCreationTests.CastleVisualCreationTests
{
    public class CastleVisualCreatorTests
    {
        [Test]
        public void GetCastleView_ShouldReturnInstantiatedPrefab_FromProvider()
        {
            var prefab = GetCastleVisual();
            var expectedInstantiatedPrefab = GetCastleVisual();
            var prefabProviderMock = new Mock<ICastleVisualPrefabProvider>();
            prefabProviderMock.Setup(c => c.GetCastleVisualPrefab()).Returns(prefab);
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(i => i.Instantiate(prefab)).Returns(expectedInstantiatedPrefab);
            var castleVisualCreator = new CastleVisualCreatorBuilder
            {
                Instantiator = instantiatorMock.Object,
                PrefabProvider = prefabProviderMock.Object
            }.Build();

            var createdCastleView = castleVisualCreator.GetCastleVisual(GetCastle());
            
            Assert.AreSame(expectedInstantiatedPrefab, createdCastleView);
        }

        [Test]
        public void GetCastleView_ShouldSetColorFromProvider_AccordingToCastleOwner()
        {
            var expectedColor = Random.ColorHSV();
            var castle = GetCastle();
            var castleVisualMock = new Mock<CastleVisual>();
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(i => i.Instantiate(It.IsAny<CastleVisual>())).Returns(castleVisualMock.Object);
            var colorProviderMock = new Mock<IPlayerContentColorProvider>();
            colorProviderMock.Setup(c => c.GetContentColor(castle.GetOwner())).Returns(expectedColor);
            var castleVisualCreator = new CastleVisualCreatorBuilder
            {
                Instantiator = instantiatorMock.Object,
                ColorProvider = colorProviderMock.Object
            }.Build();

            var createdCastleVisual = castleVisualCreator.GetCastleVisual(castle);

            castleVisualMock.Verify(c => c.SetColor(expectedColor), Times.Once);
        }
        
        private class CastleVisualCreatorBuilder
        {
            public ICastleVisualPrefabProvider PrefabProvider { get; set; }
            public IPlayerContentColorProvider ColorProvider { get; set; }
            public IInstantiator Instantiator { get; set; }

            public CastleVisualCreatorBuilder()
            {
                var prefab = GetCastleVisual();
                var prefabProviderMock = new Mock<ICastleVisualPrefabProvider>();
                prefabProviderMock.Setup(p => p.GetCastleVisualPrefab()).Returns(prefab);
                PrefabProvider = prefabProviderMock.Object;
                var colorProviderMock = new Mock<IPlayerContentColorProvider>();
                colorProviderMock.Setup(c => c.GetContentColor(It.IsAny<Player>())).Returns(Color.black);
                ColorProvider = colorProviderMock.Object;
                var instantiatedPrefab = GetCastleVisual();
                var instantiatorMock = new Mock<IInstantiator>();
                instantiatorMock.Setup(i => i.Instantiate(prefab)).Returns(instantiatedPrefab);
                Instantiator = instantiatorMock.Object;
            }
            
            public CastleVisualCreator Build()
            {
                return new CastleVisualCreator(PrefabProvider, ColorProvider, Instantiator);
            }
        }
    }
}