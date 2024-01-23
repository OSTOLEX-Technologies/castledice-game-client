using castledice_game_logic;
using castledice_game_logic.GameObjects;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation;
using Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation;
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


        [Test]
        public void GetKnightView_ShouldReturnView_WithAppropriateVisual()
        {
            var knight = GetKnight();
            var expectedVisual = GetKnightVisual();
            var visualCreatorMock = new Mock<IKnightVisualCreator>();
            visualCreatorMock.Setup(c => c.GetKnightVisual(knight)).Returns(expectedVisual);
            var factory = new KnightViewFactoryBuilder
            {
                VisualCreator = visualCreatorMock.Object
            }.Build();
            
            var knightView = factory.GetKnightView(knight);
            var fieldInfo = typeof(KnightView).GetField("_visual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualVisual = fieldInfo?.GetValue(knightView);
            
            Assert.AreSame(expectedVisual, actualVisual);
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
            public IKnightVisualCreator VisualCreator { get; set; }
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
                rotationProviderMock.Setup(provider => provider.GetRotation(It.IsAny<Player>())).Returns(Random.insideUnitSphere);
                var visualCreatorMock = new Mock<IKnightVisualCreator>();
                visualCreatorMock.Setup(c => c.GetKnightVisual(It.IsAny<Knight>())).Returns(GetKnightVisual());
                VisualCreator = visualCreatorMock.Object;
                var audioFactoryMock = new Mock<IKnightAudioFactory>();
                var audio = new GameObject().AddComponent<KnightAudioForTests>();;
                audioFactoryMock.Setup(factory => factory.GetAudio(It.IsAny<Knight>())).Returns(audio);
                AudioFactory = audioFactoryMock.Object;
            }
            
            public KnightViewFactory Build()
            {
                return new KnightViewFactory(VisualCreator, AudioFactory, KnightViewPrefab, Instantiator);
            }
        }
    }
}