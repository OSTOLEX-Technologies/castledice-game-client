using castledice_game_logic.GameObjects;
using Moq;
using NUnit.Framework;
using Src.GameplayView.ContentVisuals;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.ContentVisuals.VisualsCreation;
using Src.GameplayView.DestroyedContent;
using Src.GameplayView.Grid;
using Tests.Utils.Mocks;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayViewTests.DestroyedContentTests
{
    public class DestroyedContentViewTests
    {
        [Test]
        public void ShowDestroyedContent_ShouldPlaceVisualGameObjectFromCreatorOnGridCell_WithGivenPosition()
        {
            var position = GetRandomVector2Int();
            var gridMock = new Mock<IGrid>();
            var cellMock = new Mock<IGridCell>();
            gridMock.Setup(grid => grid.GetCell(position)).Returns(cellMock.Object);
            var visualCreatorMock = new Mock<IContentVisualCreator>();
            var content = GetCellContent();
            var visual = GetContentVisual();
            visualCreatorMock.Setup(creator => creator.GetVisual(content)).Returns(visual);
            var view = new DestroyedContentViewBuilder
            {
                Grid = gridMock.Object,
                VisualCreator = visualCreatorMock.Object
            }.Build();
            
            view.ShowDestroyedContent(position, content);
            
            cellMock.Verify(cell => cell.AddChild(visual.gameObject));
        }

        [Test]
        public void RemoveDestroyedContent_ShouldRemoveVisualGameObjectFromCell_IfItWasPreviouslyAdded()
        {
            var position = GetRandomVector2Int();
            var gridMock = new Mock<IGrid>();
            var cellMock = new Mock<IGridCell>();
            gridMock.Setup(grid => grid.GetCell(position)).Returns(cellMock.Object);
            var visualCreatorMock = new Mock<IContentVisualCreator>();
            var content = GetCellContent();
            var visual = GetContentVisual();
            visualCreatorMock.Setup(creator => creator.GetVisual(content)).Returns(visual);
            var view = new DestroyedContentViewBuilder
            {
                Grid = gridMock.Object,
                VisualCreator = visualCreatorMock.Object
            }.Build();
            
            view.ShowDestroyedContent(position, content);
            view.RemoveDestroyedContent(position, content);
            
            cellMock.Verify(cell => cell.RemoveChild(visual.gameObject));
        }

        [Test]
        public void ShowDestroyedContent_ShouldSetValueFromConfig_ToContentVisualTransparency()
        {
            var expectedTransparency = Random.value;
            var visual = new GameObject().AddComponent<ContentVisualForTests>();
            var visualCreatorMock = new Mock<IContentVisualCreator>();
            visualCreatorMock.Setup(creator => creator.GetVisual(It.IsAny<Content>())).Returns(visual);
            var transparencyConfigMock = new Mock<ITransparencyConfig>();
            transparencyConfigMock.Setup(config => config.GetTransparency()).Returns(expectedTransparency);
            var view = new DestroyedContentViewBuilder
            {
                VisualCreator = visualCreatorMock.Object,
                TransparencyConfig = transparencyConfigMock.Object
            }.Build();
            
            view.ShowDestroyedContent(GetRandomVector2Int(), GetCellContent());
            
            Assert.AreEqual(expectedTransparency, visual.Transparency);
        }
        
        private class DestroyedContentViewBuilder
        {
            public IGrid Grid { get; set; }
            public IContentVisualCreator VisualCreator { get; set; }
            public ITransparencyConfig TransparencyConfig { get; set; } = new Mock<ITransparencyConfig>().Object;

            public DestroyedContentViewBuilder()
            {
                var visual = GetContentVisual();
                var visualCreatorMock = new Mock<IContentVisualCreator>();
                visualCreatorMock.Setup(creator => creator.GetVisual(It.IsAny<Content>())).Returns(visual);
                var gridCell = new Mock<IGridCell>().Object;
                var gridMock = new Mock<IGrid>();
                gridMock.Setup(grid => grid.GetCell(It.IsAny<Vector2Int>())).Returns(gridCell);
                Grid = gridMock.Object;
                VisualCreator = visualCreatorMock.Object;
            }
            
            public DestroyedContentView Build()
            {
                return new DestroyedContentView(Grid, VisualCreator, TransparencyConfig);
            }
        }
    }
}