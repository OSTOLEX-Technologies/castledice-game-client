using System;
using System.Collections.Generic;
using Moq;
using static Tests.ObjectCreationUtility;
using NUnit.Framework;
using Src.GameplayView.Grid;
using Src.GameplayView.PlayerObjectsColor;
using Src.GameplayView.UnitsUnderlines;
using Tests.Utils.Mocks;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayViewTests.UnitsUnderlinesTests
{
    public class UnitsUnderlinesViewTests
    {
        [Test]
        public void Constructor_ShouldAddAndHideUnderlinesFromCreator_OnEachCellOfTheGrid()
        {
            var gridMock = new Mock<IGrid>();
            var cellsMocks = GetGridCellMocksList(Random.Range(1, 10));
            for (int i = 0; i < cellsMocks.Count; i++)
            {
                cellsMocks[i].Setup(cellMock => cellMock.Position).Returns(new Vector2Int(i, i));
            }
            var cells = new List<IGridCell>();
            foreach (var cellMock in cellsMocks)
            {
                cells.Add(cellMock.Object);
            }
            gridMock.Setup(gridMock => gridMock.GetEnumerator()).Returns(cells.GetEnumerator());
            var underlineCreatorMock = new Mock<IUnderlineCreator>();
            var underline = new GameObject().AddComponent<UnderlineForTests>();
            underlineCreatorMock.Setup(u => u.GetUnderline()).Returns(underline);
            
            var view = new UnitsUnderlinesViewBuilder
            {
                Grid = gridMock.Object,
                UnderlineCreator = underlineCreatorMock.Object
            }.Build();
            
            foreach (var cellMock in cellsMocks)
            {
                cellMock.Verify(cellMock => cellMock.AddChild(underline.gameObject), Times.Once);
            }
        }

        [Test]
        public void Constructor_ShouldHideAddedUnderlines()
        {
            var gridMock = new Mock<IGrid>();
            var cells = new List<IGridCell> { new Mock<IGridCell>().Object };
            gridMock.Setup(gridMock => gridMock.GetEnumerator()).Returns(cells.GetEnumerator());
            var underlineCreatorMock = new Mock<IUnderlineCreator>();
            var underline = new GameObject().AddComponent<UnderlineForTests>();
            underline.Show();
            underlineCreatorMock.Setup(u => u.GetUnderline()).Returns(underline);
            
            var view = new UnitsUnderlinesViewBuilder
            {
                Grid = gridMock.Object,
                UnderlineCreator = underlineCreatorMock.Object
            }.Build();

            Assert.IsFalse(underline.IsShown);
        }

        [Test]
        public void ShowUnderline_ShouldShowUnderline_OnGivenPosition()
        {
            //Setting up grid so that the second cell on it has needed position
            var gridMock = new Mock<IGrid>();
            var cellMock = new Mock<IGridCell>();
            var position = new Vector2Int(Random.Range(1, 10), Random.Range(1, 10));
            cellMock.Setup(cellMock => cellMock.Position).Returns(position);
            var cells = new List<IGridCell> { new Mock<IGridCell>().Object, cellMock.Object };
            gridMock.Setup(gridMock => gridMock.GetEnumerator()).Returns(cells.GetEnumerator());
            
            //Setting up underline creator so that it returns needed underline on the second call
            var underlineCreatorMock = new Mock<IUnderlineCreator>();
            var firstUnderline = new GameObject().AddComponent<UnderlineForTests>();
            var secondUnderline = new GameObject().AddComponent<UnderlineForTests>();
            var underlineCreatorCallsCount = 0;
            underlineCreatorMock.Setup(u => u.GetUnderline()).Returns(GetNextUnderline);

            //Creating view
            var view = new UnitsUnderlinesViewBuilder
            {
                Grid = gridMock.Object,
                UnderlineCreator = underlineCreatorMock.Object
            }.Build();
            
            view.ShowUnderline(position, GetPlayer());
            
            Assert.IsFalse(firstUnderline.IsShown);
            Assert.IsTrue(secondUnderline.IsShown);
            return;
            
            //Local function to return needed underline on the second call
            Underline GetNextUnderline()
            {
                underlineCreatorCallsCount++;
                return underlineCreatorCallsCount == 1 ? firstUnderline : secondUnderline;
            }
        }

        [Test]
        public void ShowUnderline_ShouldSetUnderlineColor_AccordingToPlayerColorFromProvider()
        {
            var gridMock = new Mock<IGrid>();
            var cellMock = new Mock<IGridCell>();
            var position = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            cellMock.Setup(cellMock => cellMock.Position).Returns(position);
            var cells = new List<IGridCell> { cellMock.Object };
            gridMock.Setup(gridMock => gridMock.GetEnumerator()).Returns(cells.GetEnumerator());
            var underlineCreatorMock = new Mock<IUnderlineCreator>();
            var underline = new GameObject().AddComponent<UnderlineForTests>();
            underlineCreatorMock.Setup(u => u.GetUnderline()).Returns(underline);
            var player = GetPlayer();
            var colorProviderMock = new Mock<IPlayerObjectsColorProvider>();
            var expectedColor = Random.ColorHSV();
            colorProviderMock.Setup(colorProviderMock => colorProviderMock.GetColor(player)).Returns(expectedColor);
            var view = new UnitsUnderlinesViewBuilder
            {
                Grid = gridMock.Object,
                UnderlineCreator = underlineCreatorMock.Object,
                ColorProvider = colorProviderMock.Object
            }.Build();
            
            view.ShowUnderline(position, player);
            
            Assert.AreEqual(expectedColor, underline.Color);
        }

        [Test]
        public void HideUnderline_ShouldHideUnderline_OnGivenPosition()
        {
            //Setting up grid so that the second cell on it has needed position
            var gridMock = new Mock<IGrid>();
            var cellMock = new Mock<IGridCell>();
            var position = new Vector2Int(Random.Range(1, 10), Random.Range(1, 10));
            cellMock.Setup(cellMock => cellMock.Position).Returns(position);
            var cells = new List<IGridCell> { new Mock<IGridCell>().Object, cellMock.Object };
            gridMock.Setup(gridMock => gridMock.GetEnumerator()).Returns(cells.GetEnumerator());
            
            //Setting up underline creator so that it returns needed underline on the second call
            var underlineCreatorMock = new Mock<IUnderlineCreator>();
            var firstUnderline = new GameObject().AddComponent<UnderlineForTests>();
            var secondUnderline = new GameObject().AddComponent<UnderlineForTests>();
            var underlineCreatorCallsCount = 0;
            underlineCreatorMock.Setup(u => u.GetUnderline()).Returns(GetNextUnderline);

            //Creating view
            var view = new UnitsUnderlinesViewBuilder
            {
                Grid = gridMock.Object,
                UnderlineCreator = underlineCreatorMock.Object
            }.Build();
            
            //Explicitly showing underlines as at this moment they should be hidden by view constructor
            firstUnderline.Show();
            secondUnderline.Show();
            
            view.HideUnderline(position);
            
            Assert.IsTrue(firstUnderline.IsShown);
            Assert.IsFalse(secondUnderline.IsShown);
            return;
            
            //Local function to return needed underline on the second call
            Underline GetNextUnderline()
            {
                underlineCreatorCallsCount++;
                return underlineCreatorCallsCount == 1 ? firstUnderline : secondUnderline;
            }
        }
        
        private class UnitsUnderlinesViewBuilder
        {
            public IGrid Grid { get; set; }
            public IUnderlineCreator UnderlineCreator { get; set; } = new Mock<IUnderlineCreator>().Object;
            public IPlayerObjectsColorProvider ColorProvider { get; set; } = new Mock<IPlayerObjectsColorProvider>().Object;

            public UnitsUnderlinesViewBuilder()
            {
                var gridMock = new Mock<IGrid>();
                gridMock.Setup(gridMock => gridMock.GetEnumerator()).Returns(new List<IGridCell>().GetEnumerator());
                Grid = gridMock.Object;
            }
            
            public UnitsUnderlinesView Build()
            {
                return new UnitsUnderlinesView(Grid, UnderlineCreator, ColorProvider);
            }
        }
    }
}