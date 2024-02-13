using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.Grid;
using Src.GameplayView.Highlights;
using Src.GameplayView.NewUnitsHighlights;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.PlayerObjectsColor;
using Tests.Utils.Mocks;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayViewTests.NewUnitsHighlightsTests
{
    public class NewUnitsHighlightsViewTests
    {
        [Test]
        public void Constructor_ShouldAddAndHideHighlightsFromCreator_OnEachCellOfTheGrid()
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
            var highlightCreatorMock = new Mock<IColoredHighlightCreator>();
            var highlight = new GameObject().AddComponent<ColoredHighlightForTests>();
            highlightCreatorMock.Setup(u => u.GetHighlight()).Returns(highlight);
            
            var view = new NewUnitsHighlightsViewBuilder
            {
                Grid = gridMock.Object,
                ColoredHighlightCreator = highlightCreatorMock.Object
            }.Build();
            
            foreach (var cellMock in cellsMocks)
            {
                cellMock.Verify(cellMock => cellMock.AddChild(highlight.gameObject), Times.Once);
            }
        }

        [Test]
        public void Constructor_ShouldHideAddedHighlights()
        {
            var gridMock = new Mock<IGrid>();
            var cells = new List<IGridCell> { new Mock<IGridCell>().Object };
            gridMock.Setup(gridMock => gridMock.GetEnumerator()).Returns(cells.GetEnumerator());
            var highlightCreatorMock = new Mock<IColoredHighlightCreator>();
            var highlight = new GameObject().AddComponent<ColoredHighlightForTests>();
            highlight.Show();
            highlightCreatorMock.Setup(u => u.GetHighlight()).Returns(highlight);
            
            var view = new NewUnitsHighlightsViewBuilder
            {
                Grid = gridMock.Object,
                ColoredHighlightCreator = highlightCreatorMock.Object
            }.Build();

            Assert.IsFalse(highlight.IsShown);
        }

        [Test]
        public void ShowHighlight_ShouldShowHighlight_OnGivenPosition()
        {
            //Setting up grid so that the second cell on it has needed position
            var gridMock = new Mock<IGrid>();
            var cellMock = new Mock<IGridCell>();
            var position = new Vector2Int(Random.Range(1, 10), Random.Range(1, 10));
            cellMock.Setup(cellMock => cellMock.Position).Returns(position);
            var cells = new List<IGridCell> { new Mock<IGridCell>().Object, cellMock.Object };
            gridMock.Setup(gridMock => gridMock.GetEnumerator()).Returns(cells.GetEnumerator());
            
            //Setting up highlight creator so that it returns needed highlight on the second call
            var highlightCreatorMock = new Mock<IColoredHighlightCreator>();
            var firstHighlight = new GameObject().AddComponent<ColoredHighlightForTests>();
            var secondHighlight = new GameObject().AddComponent<ColoredHighlightForTests>();
            var highlightCreatorCallsCount = 0;
            highlightCreatorMock.Setup(u => u.GetHighlight()).Returns(GetNextHighlight);

            //Creating view
            var view = new NewUnitsHighlightsViewBuilder
            {
                Grid = gridMock.Object,
                ColoredHighlightCreator = highlightCreatorMock.Object
            }.Build();
            
            view.ShowHighlight(position, GetPlayer());
            
            Assert.IsFalse(firstHighlight.IsShown);
            Assert.IsTrue(secondHighlight.IsShown);
            return;
            
            //Local function to return needed highlight on the second call
            ColoredHighlight GetNextHighlight()
            {
                highlightCreatorCallsCount++;
                return highlightCreatorCallsCount == 1 ? firstHighlight : secondHighlight;
            }
        }

        [Test]
        public void ShowHighlight_ShouldSetHighlightColor_AccordingToPlayerColorFromProvider()
        {
            var gridMock = new Mock<IGrid>();
            var cellMock = new Mock<IGridCell>();
            var position = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            cellMock.Setup(cellMock => cellMock.Position).Returns(position);
            var cells = new List<IGridCell> { cellMock.Object };
            gridMock.Setup(gridMock => gridMock.GetEnumerator()).Returns(cells.GetEnumerator());
            var highlightCreatorMock = new Mock<IColoredHighlightCreator>();
            var highlight = new GameObject().AddComponent<ColoredHighlightForTests>();
            highlightCreatorMock.Setup(u => u.GetHighlight()).Returns(highlight);
            var player = GetPlayer();
            var colorProviderMock = new Mock<IPlayerObjectsColorProvider>();
            var expectedColor = Random.ColorHSV();
            colorProviderMock.Setup(colorProviderMock => colorProviderMock.GetColor(player)).Returns(expectedColor);
            var view = new NewUnitsHighlightsViewBuilder
            {
                Grid = gridMock.Object,
                ColoredHighlightCreator = highlightCreatorMock.Object,
                ColorProvider = colorProviderMock.Object
            }.Build();
            
            view.ShowHighlight(position, player);
            
            Assert.AreEqual(expectedColor, highlight.Color);
        }

        [Test]
        public void HideHighlights_ShouldHideAllHighlights()
        {
            //Setting up grid
            var gridMock = new Mock<IGrid>();
            var cellsMocks = GetGridCellMocksList(10);
            for (int i = 0; i < cellsMocks.Count; i++)
            {
                cellsMocks[i].Setup(cellMock => cellMock.Position).Returns(new Vector2Int(i, i));
            }
            var cells = new List<IGridCell>();
            foreach (var cellMock in cellsMocks)
            {
                cells.Add(cellMock.Object);
            }
            gridMock.Setup(g => g.GetEnumerator()).Returns(cells.GetEnumerator());
            
            //Setting up highlights creator
            var highlightCreatorMock = new Mock<IColoredHighlightCreator>();
            var highlights = new List<ColoredHighlightForTests>();
            for (int i = 0; i < 10; i++)
            {
                highlights.Add(new GameObject().AddComponent<ColoredHighlightForTests>());
            }
            var highlightCreatorCallsCount = 0;
            highlightCreatorMock.Setup(u => u.GetHighlight()).Returns(GetNextHighlight);

            //Creating view
            var view = new NewUnitsHighlightsViewBuilder
            {
                Grid = gridMock.Object,
                ColoredHighlightCreator = highlightCreatorMock.Object
            }.Build();
            
            //Showing highlights
            foreach (var highlight in highlights)
            {
                highlight.Show();
            }
            
            view.HideHighlights();
            
            foreach (var highlight in highlights)
            {
                Assert.IsFalse(highlight.IsShown);
            }

            return;

            ColoredHighlight GetNextHighlight()
            {
                var highlight = highlights[highlightCreatorCallsCount];
                highlightCreatorCallsCount++;
                return highlight;
            }
        }

        [Test]
        public void ShowHighlight_ShouldThrowInvalidOperationException_IfNoHighlightOnGivenPosition()
        {
            var view = new NewUnitsHighlightsViewBuilder().Build();
            Assert.Throws<InvalidOperationException>(() => view.ShowHighlight(new Vector2Int(0, 0), GetPlayer()));
        }
        
        private class NewUnitsHighlightsViewBuilder
        {
            public IGrid Grid { get; set; }
            public IColoredHighlightCreator ColoredHighlightCreator { get; set; } = new Mock<IColoredHighlightCreator>().Object;
            public IPlayerObjectsColorProvider ColorProvider { get; set; } = new Mock<IPlayerObjectsColorProvider>().Object;

            public NewUnitsHighlightsViewBuilder()
            {
                var gridMock = new Mock<IGrid>();
                gridMock.Setup(gridMock => gridMock.GetEnumerator()).Returns(new List<IGridCell>().GetEnumerator());
                Grid = gridMock.Object;
            }
            
            public NewUnitsHighlightsView Build()
            {
                return new NewUnitsHighlightsView(Grid, ColoredHighlightCreator, ColorProvider);
            }
        }
    }
}