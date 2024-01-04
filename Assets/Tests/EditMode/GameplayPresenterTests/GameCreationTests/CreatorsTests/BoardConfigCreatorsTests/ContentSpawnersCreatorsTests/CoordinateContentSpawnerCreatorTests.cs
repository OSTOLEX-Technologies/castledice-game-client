using System.Collections.Generic;
using castledice_game_data_logic.Content;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreatorsTests.BoardConfigCreatorsTests.ContentSpawnersCreatorsTests
{
    public class CoordinateContentSpawnerCreatorTests
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
            var contentToCoordinateCreatorMock = new Mock<IContentToCoordinateCreator>();
            contentToCoordinateCreatorMock.Setup(creator => creator.GetContentToCoordinate(It.IsAny<ContentData>(), It.IsAny<List<Player>>()))
                .Returns(new ContentToCoordinate((0, 0), GetCellContent()));
            var spawnerCreator = new CoordinateContentSpawnerCreator(contentToCoordinateCreatorMock.Object);
            
            var spawners = spawnerCreator.GetContentSpawnersList(new List<ContentData>(), new List<Player>());
            
            Assert.AreEqual(1, spawners.Count);
            Assert.IsInstanceOf<CoordinateContentSpawner>(spawners[0]);
        }
        
        [Test]
        public void GetContentSpawners_ShouldPassEveryContentData_ToContentToCoordinateCreator([ValueSource(nameof(ContentDataLists))]List<ContentData> contentData)
        {
            var contentToCoordinateCreatorMock = new Mock<IContentToCoordinateCreator>();
            contentToCoordinateCreatorMock
                .Setup(c => c.GetContentToCoordinate(It.IsAny<ContentData>(), It.IsAny<List<Player>>()))
                .Returns(new ContentToCoordinate((0, 0), GetCellContent()));
            var spawnerCreator = new CoordinateContentSpawnerCreator(contentToCoordinateCreatorMock.Object);
            
            spawnerCreator.GetContentSpawnersList(contentData, new List<Player>());
            
            foreach (var data in contentData)
            {
                contentToCoordinateCreatorMock.Verify(p => p.GetContentToCoordinate(data, It.IsAny<List<Player>>()), Times.Once);
            }
        }
        
        [TestCaseSource(nameof(ContentToCoordinateTestCases))]
        public void ReturnedSpawner_ShouldHaveListOfContentToCoordinateObjects_FromGivenContentToCoordinateCreator(List<ContentData> contentData, List<ContentToCoordinate> expectedContentToCoordinates)
        {
            var contentToCoordinateCreatorMock = new Mock<IContentToCoordinateCreator>();
            for (int i = 0; i < contentData.Count; i++)
            {
                contentToCoordinateCreatorMock.Setup(creator => creator.GetContentToCoordinate(contentData[i], It.IsAny<List<Player>>()))
                    .Returns(expectedContentToCoordinates[i]);
            }
            var spawnerCreator = new CoordinateContentSpawnerCreator(contentToCoordinateCreatorMock.Object);
            
            var spawners = spawnerCreator.GetContentSpawnersList(contentData, new List<Player>());
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