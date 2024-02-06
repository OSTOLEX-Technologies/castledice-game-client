using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation;
using Src.GameplayView.ContentVisuals;
using Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation;
using Tests.Utils.Mocks;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsCreationTests.CastleViewCreationTests
{
    public class CastleViewFactoryTests
    {
        [Test]
        public void GetCastleView_ShouldReturnInstantiatedCastleViewPrefab()
        {
            var prefab = new GameObject().AddComponent<CastleView>();
            var instantiatedPrefab = new GameObject().AddComponent<CastleView>();
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(instantiator => instantiator.Instantiate(prefab)).Returns(instantiatedPrefab);
            var factory = new CastleViewFactoryBuilder
            {
                CastleViewPrefab = prefab,
                Instantiator = instantiatorMock.Object
            }.Build();
            
            var view = factory.GetCastleView(GetCastle());
            
            Assert.AreSame(instantiatedPrefab, view);
        }

        [Test]
        public void GetCastleView_ShouldReturnViewWithAudio_FromGivenAudioFactory()
        {
            var expectedAudio = new GameObject().AddComponent<CastleAudioForTests>();
            var audioFactoryMock = new Mock<ICastleAudioFactory>();
            var castle = GetCastle();
            audioFactoryMock.Setup(factory => factory.GetAudio(castle)).Returns(expectedAudio);
            var factory = new CastleViewFactoryBuilder
            {
                CastleAudioFactory = audioFactoryMock.Object
            }.Build();
            
            var view = factory.GetCastleView(castle);
            var fieldInfo = view.GetType().GetField("_audio", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualAudio = fieldInfo.GetValue(view) as CastleAudioForTests;
            
            Assert.AreSame(expectedAudio, actualAudio);
        }

        [Test]
        public void GetCastleView_ShouldReturnViewWithVisual_FromGivenCreator()
        {
            var expectedVisual = GetCastleVisual();
            var visualCreatorMock = new Mock<ICastleVisualCreator>();
            var castle = GetCastle();
            visualCreatorMock.Setup(c => c.GetCastleVisual(castle)).Returns(expectedVisual);
            var factory = new CastleViewFactoryBuilder
            {
                VisualCreator = visualCreatorMock.Object
            }.Build();
            
            var view = factory.GetCastleView(castle);
            var fieldInfo = view.GetType().GetField("_visual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualVisual = fieldInfo.GetValue(view) as CastleVisual;
            
            Assert.AreSame(expectedVisual, actualVisual);
        }

        private class CastleViewFactoryBuilder
        {
            public ICastleVisualCreator VisualCreator { get; set; }
            public ICastleAudioFactory CastleAudioFactory { get; set; }
            public CastleView CastleViewPrefab { get; set; }
            public IInstantiator Instantiator { get; set; }

            public CastleViewFactoryBuilder()
            {
                var visual = GetCastleVisual();
                var visualCreatorMock = new Mock<ICastleVisualCreator>();
                visualCreatorMock.Setup(provider => provider.GetCastleVisual(It.IsAny<CastleGO>())).Returns(visual);
                VisualCreator = visualCreatorMock.Object;
                var audio = new GameObject().AddComponent<CastleAudioForTests>();
                var audioFactoryMock = new Mock<ICastleAudioFactory>();
                audioFactoryMock.Setup(factory => factory.GetAudio(It.IsAny<CastleGO>())).Returns(audio);
                CastleAudioFactory = audioFactoryMock.Object;
                var prefab = new GameObject().AddComponent<CastleView>();
                CastleViewPrefab = prefab;
                var instantiatedPrefab = new GameObject().AddComponent<CastleView>();
                var instantiatorMock = new Mock<IInstantiator>();
                instantiatorMock.Setup(instantiator => instantiator.Instantiate(prefab)).Returns(instantiatedPrefab);
                Instantiator = instantiatorMock.Object;
            }
            
            public CastleViewFactory Build()
            {
                return new CastleViewFactory(VisualCreator, CastleAudioFactory, CastleViewPrefab, Instantiator);
            }
        }
    }
}