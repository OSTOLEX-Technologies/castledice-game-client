using castledice_game_logic;
using castledice_game_logic.GameObjects;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersRotations;
using Tests.Utils.Mocks;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsCreationTests.KnightViewCreationTests
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
            var fieldInfo = typeof(KnightView).GetField("Model", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
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
            var fieldInfo = typeof(KnightView).GetField("Model", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var model = fieldInfo?.GetValue(knightView);
            var actualRotation = ((GameObject) model).transform.localRotation.eulerAngles;
            
            Assert.AreEqual(expectedRotation.x, actualRotation.x, 0.0001f);
            Assert.AreEqual(expectedRotation.y, actualRotation.y, 0.0001f);
            Assert.AreEqual(expectedRotation.z, actualRotation.z, 0.0001f);
        }

        //In this test word "appropriate" means that the audio corresponds to knight, that is, obtained from the audio factory by passing Knight from view to it.
        [Test]
        public void GetKnightView_ShouldReturnView_WithAppropriateKnightAudio()
        {
            var knight = GetKnight();
            var audioFactoryMock = new Mock<IKnightAudioFactory>();
            var expectedAudio = new GameObject().AddComponent<KnightAudioForTests>();;
            audioFactoryMock.Setup(f => f.GetAudio(knight)).Returns(expectedAudio);
            var factory = new KnightViewFactoryBuilder
            {
                AudioFactory = audioFactoryMock.Object
            }.Build();
            
            var knightView = factory.GetKnightView(knight);
            var fieldInfo = typeof(KnightView).GetField("_audio", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualAudio = fieldInfo?.GetValue(knightView);
            
            Assert.AreSame(expectedAudio, actualAudio);
        }

        private class KnightViewFactoryBuilder
        {
            public IPlayerRotationProvider RotationProvider { get; set; }
            public IPlayerColorProvider ColorProvider { get; set; }
            public IKnightModelProvider ModelProvider { get; set; }
            public IKnightAudioFactory AudioFactory { get; set; }
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
                var audioFactoryMock = new Mock<IKnightAudioFactory>();
                var audio = new GameObject().AddComponent<KnightAudioForTests>();;
                audioFactoryMock.Setup(factory => factory.GetAudio(It.IsAny<Knight>())).Returns(audio);
                AudioFactory = audioFactoryMock.Object;
            }
            
            public KnightViewFactory Build()
            {
                return new KnightViewFactory(RotationProvider, ColorProvider, ModelProvider, AudioFactory, KnightViewPrefab, Instantiator);
            }
        }
    }
}