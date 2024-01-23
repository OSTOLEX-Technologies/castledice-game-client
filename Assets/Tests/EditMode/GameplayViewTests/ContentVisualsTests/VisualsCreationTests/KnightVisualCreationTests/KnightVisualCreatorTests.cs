using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.ContentVisuals;
using Src.GameplayView.ContentVisuals.ContentColor;
using Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation;
using Src.GameplayView.PlayersRotations;
using Tests.Utils.Mocks;
using UnityEngine;
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
            var knightVisual = new GameObject().AddComponent<KnightVisualForTests>();
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(x => x.Instantiate(It.IsAny<KnightVisual>())).Returns(knightVisual);
            var playerContentColorProviderMock = new Mock<IPlayerContentColorProvider>();
            playerContentColorProviderMock.Setup(x => x.GetContentColor(knight.GetOwner())).Returns(expectedColor);
            var knightVisualCreator = new KnightVisualCreatorBuilder
            {
                Instantiator = instantiatorMock.Object,
                ColorProvider = playerContentColorProviderMock.Object
            }.Build();
            
            var actualKnightVisual = knightVisualCreator.GetKnightVisual(knight);
            
            Assert.AreEqual(expectedColor, knightVisual.Color);
        }
        
        [Test]
        public void GetKnightVisual_ShouldSetKnightVisualRotation_WithRotationFromProvider()
        {
            var knight = GetKnight();
            var expectedRotation = new Vector3(Random.value, Random.value, Random.value);
            var knightVisual = GetKnightVisual();
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(x => x.Instantiate(It.IsAny<KnightVisual>())).Returns(knightVisual);
            var playerRotationProviderMock = new Mock<IPlayerRotationProvider>();
            playerRotationProviderMock.Setup(x => x.GetRotation(knight.GetOwner())).Returns(expectedRotation);
            var knightVisualCreator = new KnightVisualCreatorBuilder
            {
                Instantiator = instantiatorMock.Object,
                RotationProvider = playerRotationProviderMock.Object
            }.Build();
            
            var actualKnightVisual = knightVisualCreator.GetKnightVisual(knight);
            var actualRotation = actualKnightVisual.transform.localEulerAngles;
            
            Assert.AreEqual(expectedRotation.x, actualRotation.x, 0.01f);
            Assert.AreEqual(expectedRotation.y, actualRotation.y, 0.01f);
            Assert.AreEqual(expectedRotation.z, actualRotation.z, 0.01f);
        }
        
        private class KnightVisualCreatorBuilder
        {
            public IKnightVisualPrefabProvider PrefabProvider { get; set; }
            public IPlayerContentColorProvider ColorProvider { get; set; }
            public IPlayerRotationProvider RotationProvider { get; set; }
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
                var rotationProviderMock = new Mock<IPlayerRotationProvider>();
                rotationProviderMock.Setup(x => x.GetRotation(It.IsAny<Player>())).Returns(Vector3.zero);
                RotationProvider = rotationProviderMock.Object;
            }
            
            public KnightVisualCreator Build()
            {
                return new KnightVisualCreator(PrefabProvider, ColorProvider, Instantiator, RotationProvider);
            }
        }
    }
}