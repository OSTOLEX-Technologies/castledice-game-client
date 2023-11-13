using System.Collections.Generic;
using castledice_game_data_logic.Content;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode
{
    public class CoordinateContentSpawnerProviderTests
    {
        public static List<ContentData>[] ContentDataLists =
        {
            new () {GetCastleData()},
            new () {GetTreeData()},
            new () {GetCastleData(), GetTreeData()}
        };
        
        [Test]
        public void GetContentSpawners_ShouldReturnOneElementList_WithCoordinateContentSpawner()
        {
            var contentToCoordinateProviderMock = new Mock<IContentToCoordinateProvider>();
            contentToCoordinateProviderMock.Setup(provider => provider.GetContentToCoordinate(It.IsAny<ContentData>(), It.IsAny<List<Player>>()))
                .Returns(new ContentToCoordinate((0, 0), GetCellContent()));
            var spawnerProvider = new CoordinateContentSpawnerProvider(contentToCoordinateProviderMock.Object);
            
            var spawners = spawnerProvider.GetContentSpawnersList(new List<ContentData>(), new List<Player>());
            
            Assert.AreEqual(1, spawners.Count);
            Assert.IsInstanceOf<CoordinateContentSpawner>(spawners[0]);
        }
        
        [Test]
        public void GetContentSpawners_ShouldPassEveryContentData_ToContentToCoordinateProvider([ValueSource(nameof(ContentDataLists))]List<ContentData> contentData)
        {
            var contentToCoordinateProviderMock = new Mock<IContentToCoordinateProvider>();
            contentToCoordinateProviderMock
                .Setup(c => c.GetContentToCoordinate(It.IsAny<ContentData>(), It.IsAny<List<Player>>()))
                .Returns(new ContentToCoordinate((0, 0), GetCellContent()));
            var spawnerProvider = new CoordinateContentSpawnerProvider(contentToCoordinateProviderMock.Object);
            
            spawnerProvider.GetContentSpawnersList(contentData, new List<Player>());
            
            foreach (var data in contentData)
            {
                contentToCoordinateProviderMock.Verify(p => p.GetContentToCoordinate(data, It.IsAny<List<Player>>()), Times.Once);
            }
        }
        
        [TestCaseSource(nameof(ContentToCoordinateTestCases))]
        public void ReturnedSpawner_ShouldHaveListOfContentToCoordinateObjects_FromGivenContentToCoordinateProvider(List<ContentData> contentData, List<ContentToCoordinate> expectedContentToCoordinates)
        {
            var contentToCoordinateProviderMock = new Mock<IContentToCoordinateProvider>();
            for (int i = 0; i < contentData.Count; i++)
            {
                contentToCoordinateProviderMock.Setup(provider => provider.GetContentToCoordinate(contentData[i], It.IsAny<List<Player>>()))
                    .Returns(expectedContentToCoordinates[i]);
            }
            var spawnerProvider = new CoordinateContentSpawnerProvider(contentToCoordinateProviderMock.Object);
            
            var spawners = spawnerProvider.GetContentSpawnersList(contentData, new List<Player>());
            var spawner = spawners[0] as CoordinateContentSpawner;
            var actualContentToCoordinates = spawner.ContentToCoordinates;
            
            Assert.AreEqual(expectedContentToCoordinates.Count, actualContentToCoordinates.Count);
            foreach (var contentToCoordinate in expectedContentToCoordinates)
            {
                Assert.Contains(contentToCoordinate, actualContentToCoordinates);
            }
        }

        public static object[] ContentToCoordinateTestCases =
        {
            new object[]
            {
                new List<ContentData> { new TestContentData((1, 1)) },
                new List<ContentToCoordinate> { new ((1, 1), new TestContent()) }
            },
            new object[]
            {
                new List<ContentData> { new TestContentData((1, 1)), new TestContentData((2, 2))},
                new List<ContentToCoordinate> { new ((1, 1), new TestContent()), new ((2, 2),new TestContent()) }
            },
            new object[]
            {
                new List<ContentData> { new TestContentData((1, 1)), new TestContentData((2, 2)), new TestContentData((3, 3)) },
                new List<ContentToCoordinate> { new ((1, 1), new TestContent()), new ((2, 2), new TestContent()), new ((3, 3), new TestContent()) }
            }
        };

        private class TestContentData : ContentData
        {
            public TestContentData(Vector2Int position) : base(position)
            {
            }

            public override T Accept<T>(IContentDataVisitor<T> visitor)
            {
                throw new System.NotImplementedException();
            }

            public override ContentDataType Type { get; }
        }

        private class TestContent : Content
        {
            public override void Update()
            {
                throw new System.NotImplementedException();
            }

            public override T Accept<T>(IContentVisitor<T> visitor)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}