using System;
using System.Collections.Generic;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsCreationTests.CastleViewCreationTests
{
    public class ColoredCastleModelProviderTests
    {
        [Test]
        public void GetCastleModel_ShouldReturnInstantiatedPrefab_FromProvider()
        {
            var prefab = new GameObject();
            var instantiatedPrefab = new GameObject();
            var prefabProviderMock = new Mock<ICastleModelPrefabProvider>();
            var instantiatorMock = new Mock<IInstantiator>();
            prefabProviderMock.Setup(provider => provider.GetCastleModelPrefab(It.IsAny<PlayerColor>()))
                .Returns(prefab);
            instantiatorMock.Setup(instantiator => instantiator.Instantiate(prefab)).Returns(instantiatedPrefab);
            var castleModelProvider = new ColoredCastleModelProviderBuilder
            {
                Instantiator = instantiatorMock.Object,
                PrefabProvider = prefabProviderMock.Object
            }.Build();
            
            var model = castleModelProvider.GetCastleModel(GetCastle());
            
            Assert.AreSame(instantiatedPrefab, model);
        }

        [Test]
        [TestCaseSource(nameof(GetPlayerColors))]
        public void GetCastleModel_ShouldChoosePrefab_AccordingToColor(PlayerColor playerColor)
        {
            var prefab = new GameObject();
            var castle = GetCastle();
            var prefabProviderMock = new Mock<ICastleModelPrefabProvider>();
            prefabProviderMock.Setup(provider => provider.GetCastleModelPrefab(playerColor)).Returns(prefab);
            var colorProviderMock = new Mock<IPlayerColorProvider>();
            colorProviderMock.Setup(provider => provider.GetPlayerColor(castle.GetOwner())).Returns(playerColor);
            var castleModelProvider = new ColoredCastleModelProviderBuilder
            {
                PrefabProvider = prefabProviderMock.Object,
                ColorProvider = colorProviderMock.Object
            }.Build();
            
            var model = castleModelProvider.GetCastleModel(castle);
            
            prefabProviderMock.Verify(provider => provider.GetCastleModelPrefab(playerColor), Times.Once);
        }
        
        public static IEnumerable<PlayerColor> GetPlayerColors()
        {
            var colors = Enum.GetValues(typeof(PlayerColor));
            foreach (var color in colors)
            {
                yield return (PlayerColor) color;
            }
        }
        
        private class ColoredCastleModelProviderBuilder
        {
            public IPlayerColorProvider ColorProvider { get; set; }
            public ICastleModelPrefabProvider PrefabProvider { get; set; }
            public IInstantiator Instantiator { get; set; }

            public ColoredCastleModelProviderBuilder()
            {
                var colorProviderMock = new Mock<IPlayerColorProvider>();
                colorProviderMock.Setup(provider => provider.GetPlayerColor(It.IsAny<Player>())).Returns(PlayerColor.Red);
                ColorProvider = colorProviderMock.Object;
                var prefab = new GameObject();
                var instantiatedPrefab = new GameObject();
                var prefabProviderMock = new Mock<ICastleModelPrefabProvider>();
                prefabProviderMock.Setup(provider => provider.GetCastleModelPrefab(It.IsAny<PlayerColor>()))
                    .Returns(prefab);
                PrefabProvider = prefabProviderMock.Object;
                var instantiatorMock = new Mock<IInstantiator>();
                instantiatorMock.Setup(instantiator => instantiator.Instantiate(prefab)).Returns(instantiatedPrefab);
                Instantiator = instantiatorMock.Object;
            }
            
            public ColoredCastleModelProvider Build()
            {
                return new ColoredCastleModelProvider(ColorProvider, PrefabProvider, Instantiator);
            }
        } 
    }
}