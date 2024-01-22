using Moq;
using NUnit.Framework;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.ContentVisuals.VisualsCreation;
using Src.GameplayView.DestroyedContent;
using Src.GameplayView.Grid;
using UnityEngine;

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
            var view = new DestroyedContentView(gridMock.Object, visualCreatorMock.Object);
            
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
            var view = new DestroyedContentView(gridMock.Object, visualCreatorMock.Object);
            
            view.ShowDestroyedContent(position, content);
            view.RemoveDestroyedContent(position, content);
            
            cellMock.Verify(cell => cell.RemoveChild(visual.gameObject));
        }
    }
}