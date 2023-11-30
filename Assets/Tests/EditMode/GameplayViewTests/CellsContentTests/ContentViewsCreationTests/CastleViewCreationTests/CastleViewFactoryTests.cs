using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation;
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
        public void GetCastleView_ShouldReturnViewWithModel_FromGivenProvider()
        {
            var expectedModel = new GameObject();
            var modelProviderMock = new Mock<ICastleModelProvider>();
            var castle = GetCastle();
            modelProviderMock.Setup(provider => provider.GetCastleModel(castle)).Returns(expectedModel);
            var factory = new CastleViewFactoryBuilder
            {
                CastleModelProvider = modelProviderMock.Object
            }.Build();
            
            var view = factory.GetCastleView(castle);
            var fieldInfo = view.GetType().GetField("Model", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualModel = fieldInfo.GetValue(view) as GameObject;
            
            Assert.AreSame(expectedModel, actualModel);
        }

        private class CastleViewFactoryBuilder
        {
            public ICastleModelProvider CastleModelProvider { get; set; }
            public ICastleAudioFactory CastleAudioFactory { get; set; }
            public CastleView CastleViewPrefab { get; set; }
            public IInstantiator Instantiator { get; set; }

            public CastleViewFactoryBuilder()
            {
                var model = new GameObject();
                var modelProviderMock = new Mock<ICastleModelProvider>();
                modelProviderMock.Setup(provider => provider.GetCastleModel(It.IsAny<CastleGO>())).Returns(model);
                CastleModelProvider = modelProviderMock.Object;
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
                return new CastleViewFactory(CastleModelProvider, CastleAudioFactory, CastleViewPrefab, Instantiator);
            }
        }
    }
}