using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentCreation.KnightsCreation;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersRotations;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentCreationTests.KnightCreationTests
{
    public class KnightViewFactoryTests
    {
        [Test]
        public void GetKnightView_ShouldReturnInstance_OfKnightViewPrefab()
        {
            var instantiatorMock = new Mock<IInstantiator>();
            var knightViewPrefab = new GameObject().AddComponent<KnightView>();
            var instantiatedKnightView = new GameObject().AddComponent<KnightView>();
            instantiatorMock.Setup(instantiator => instantiator.Instantiate(knightViewPrefab)).Returns(instantiatedKnightView);
            var factory = new KnightViewFactoryBuilder
            {
                Instantiator = instantiatorMock.Object,
                KnightViewPrefab = knightViewPrefab
            }.Build();
            
            var knightView = factory.GetKnightView(GetKnight());
            
            Assert.AreSame(instantiatedKnightView, knightView);
        }

        [Test]
        public void GetKnightView_ShouldReturnView_WithGivenKnight()
        {
            var expectedKnight = GetKnight();
            var factory = new KnightViewFactoryBuilder().Build();
            
            var knightView = factory.GetKnightView(expectedKnight);
            
            Assert.AreSame(expectedKnight, knightView.Content);
        }

        //In this test word "appropriate" means that the model corresponds to player color, that is, obtained from the model provider.
        [Test]
        [TestCase(PlayerColor.Blue)]
        [TestCase(PlayerColor.Red)]
        public void GetKnightView_ShouldReturnView_WithAppropriateModel(PlayerColor playerColor)
        {
            var knight = GetKnight();
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(p => p.GetPlayerColor(knight.GetOwner())).Returns(playerColor);
            var expectedModel = new GameObject();
            var modelProviderMock = new Mock<IKnightModelProvider>();
            modelProviderMock.Setup(p => p.GetKnightModel(playerColor)).Returns(expectedModel);
            var factory = new KnightViewFactoryBuilder
            {
                ColorProvider = playerColorProviderMock.Object,
                ModelProvider = modelProviderMock.Object
            }.Build();
            
            var knightView = factory.GetKnightView(knight);
            var fieldInfo = typeof(KnightView).GetField("_model", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualModel = fieldInfo?.GetValue(knightView);
            
            Assert.AreSame(expectedModel, actualModel);
        }

        //In this test word "appropriate" means that the rotation corresponds to player color, that is, obtained from the rotation provider.
        [Test]
        [TestCase(PlayerColor.Blue)]
        [TestCase(PlayerColor.Red)]
        public void GetKnightView_ShouldReturnView_WithAppropriateModelRotation(PlayerColor playerColor)
        {
            var knight = GetKnight();
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(p => p.GetPlayerColor(knight.GetOwner())).Returns(playerColor);
            var expectedRotation = new Vector3(Random.value, Random.value, Random.value); //We pass Vector3 with only positive values because Unity normalizes rotation values to be between 0 and 360 and it may ruin the test.
            var rotationProviderMock = new Mock<IPlayerRotationProvider>();
            rotationProviderMock.Setup(p => p.GetRotation(playerColor)).Returns(expectedRotation);
            var factory = new KnightViewFactoryBuilder
            {
                ColorProvider = playerColorProviderMock.Object,
                RotationProvider = rotationProviderMock.Object
            }.Build();
            
            var knightView = factory.GetKnightView(knight);
            var fieldInfo = typeof(KnightView).GetField("_model", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var model = fieldInfo?.GetValue(knightView);
            var actualRotation = ((GameObject) model).transform.localRotation.eulerAngles;
            
            Assert.AreEqual(expectedRotation.x, actualRotation.x, 0.0001f);
            Assert.AreEqual(expectedRotation.y, actualRotation.y, 0.0001f);
            Assert.AreEqual(expectedRotation.z, actualRotation.z, 0.0001f);
        }

        private class KnightViewFactoryBuilder
        {
            public IPlayerRotationProvider RotationProvider { get; set; }
            public IPlayerColorProvider ColorProvider { get; set; }
            public IKnightModelProvider ModelProvider { get; set; }
            public KnightView KnightViewPrefab { get; set; }
            public IInstantiator Instantiator { get; set; }

            public KnightViewFactoryBuilder()
            {
                var knightViewPrefab = new GameObject().AddComponent<KnightView>();
                KnightViewPrefab = knightViewPrefab;
                var instantiatedKnightView = new GameObject().AddComponent<KnightView>();
                var instantiatorMock = new Mock<IInstantiator>();
                instantiatorMock.Setup(instantiator => instantiator.Instantiate(knightViewPrefab)).Returns(instantiatedKnightView);
                Instantiator = instantiatorMock.Object;
                var rotationProviderMock = new Mock<IPlayerRotationProvider>();
                rotationProviderMock.Setup(provider => provider.GetRotation(It.IsAny<PlayerColor>())).Returns(Random.insideUnitSphere);
                RotationProvider = rotationProviderMock.Object;
                var colorProviderMock = new Mock<IPlayerColorProvider>();
                colorProviderMock.Setup(provider => provider.GetPlayerColor(It.IsAny<Player>())).Returns(PlayerColor.Blue);
                ColorProvider = colorProviderMock.Object;
                var modelProviderMock = new Mock<IKnightModelProvider>();
                modelProviderMock.Setup(provider => provider.GetKnightModel(It.IsAny<PlayerColor>())).Returns(new GameObject());
                ModelProvider = modelProviderMock.Object;
            }
            
            public KnightViewFactory Build()
            {
                return new KnightViewFactory(RotationProvider, ColorProvider, ModelProvider, KnightViewPrefab, Instantiator);
            }
        }
    }
}