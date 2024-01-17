using System.Collections.Generic;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation;
using static Tests.ObjectCreationUtility;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests.VisualsCreationTests.TreeVisualCreation
{
    public class RandomTreeVisualCreatorTests
    {
        [Test]
        public void GetTreeVisual_ShouldReturnInstantiatedRandomPrefab_FromTreeVisualPrefabsList()
        {
            var prefabsList = GetTreeVisualsList(Random.Range(1, 10));
            int randomIndex = Random.Range(0, prefabsList.Count);
            var prefab = prefabsList[randomIndex];
            var instantiatedPrefab = GetTreeVisual();
            var prefabProviderMock = new Mock<ITreeVisualPrefabsListProvider>();
            prefabProviderMock.Setup(x => x.GetTreeVisualPrefabsList()).Returns(prefabsList);
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(x => x.Instantiate(prefab)).Returns(instantiatedPrefab);
            var rangeRandomNumberGeneratorMock = new Mock<IRangeRandomNumberGenerator>();
            rangeRandomNumberGeneratorMock.Setup(x => x.GetRandom(0, prefabsList.Count)).Returns(randomIndex);
            var treeVisualCreator = new RandomTreeVisualCreator(rangeRandomNumberGeneratorMock.Object,
                prefabProviderMock.Object, instantiatorMock.Object);
            
            var actualTreeVisual = treeVisualCreator.GetTreeVisual(GetTree());
            
            Assert.AreSame(instantiatedPrefab, actualTreeVisual);
        }
    }
}