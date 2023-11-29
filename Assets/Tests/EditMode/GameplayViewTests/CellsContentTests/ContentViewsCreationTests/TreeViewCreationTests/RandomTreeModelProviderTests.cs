using System;
using System.Collections.Generic;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.CellsContent.ContentViewsCreation.TreeViewCreation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsCreationTests.TreeViewCreationTests
{
    public class RandomTreeModelProviderTests
    {
        [Test, Repeat(10)]
        public void GetRandomTreeModel_ShouldReturnInstantiatedRandomTreeModel_FromTreeModelPrefabsList()
        {
            var prefabsCount = Random.Range(1, 10);
            var expectedPrefabIndex = Random.Range(0, prefabsCount);
            var prefabsList = new List<GameObject>();
            for (var i = 0; i < prefabsCount; i++)
            {
                prefabsList.Add(new GameObject());
            }
            var expectedPrefab = prefabsList[expectedPrefabIndex];
            var expectedTreeModel = new GameObject();
            var prefabsListProviderMock = new Mock<ITreeModelPrefabsListProvider>();
            prefabsListProviderMock.Setup(x => x.GetTreeModelPrefabsList()).Returns(prefabsList);
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(x => x.Instantiate(expectedPrefab)).Returns(expectedTreeModel);
            var rndMock = new Mock<IRangeRandomNumberGenerator>();
            rndMock.Setup(x => x.GetRandom(0, prefabsCount)).Returns(expectedPrefabIndex);
            var provider = new RandomTreeModelProvider(rndMock.Object, prefabsListProviderMock.Object, instantiatorMock.Object);
            
            var treeModel = provider.GetTreeModel();
            
            Assert.AreSame(expectedTreeModel, treeModel);
        }
        
        [Test]
        public void GetRandomTreeModel_ShouldThrowArgumentException_WhenTreeModelPrefabsListIsEmpty()
        {
            var prefabsList = new List<GameObject>();
            var prefabsListProviderMock = new Mock<ITreeModelPrefabsListProvider>();
            prefabsListProviderMock.Setup(x => x.GetTreeModelPrefabsList()).Returns(prefabsList);
            var instantiatorMock = new Mock<IInstantiator>();
            var rndMock = new Mock<IRangeRandomNumberGenerator>();
            var provider = new RandomTreeModelProvider(rndMock.Object, prefabsListProviderMock.Object, instantiatorMock.Object);
            
            Assert.Throws<ArgumentException>(() => provider.GetTreeModel());
        }
    }
}