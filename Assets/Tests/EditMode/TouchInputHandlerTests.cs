using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.ClickDetection;
using Src.PlayerInput;
using UnityEngine;

namespace Tests.EditMode
{
    public class TouchInputHandlerTests
    {
        public static Vector2[] TouchPositions =
        {
            new (0, 0),
            new (1, 1),
            new (2, 2),
            new (3, 3),
            new (4, 4),
            new (5, 5),
            new (6, 6),
            new (7, 7),
            new (8, 8),
            new (9, 9),
        };

        public static Ray[] Rays =
        {
            new (new Vector3(0, 0, 0), new Vector3(1, 1, 1)),
            new (new Vector3(1, 1, 1), new Vector3(2, 2, 2)),
            new (new Vector3(2, 2, 2), new Vector3(3, 3, 3)),
            new (new Vector3(3, 3, 3), new Vector3(4, 4, 4)),
            new (new Vector3(4, 4, 4), new Vector3(5, 5, 5)),
            new (new Vector3(5, 5, 5), new Vector3(6, 6, 6)),
            new (new Vector3(6, 6, 6), new Vector3(7, 7, 7)),
            new (new Vector3(7, 7, 7), new Vector3(8, 8, 8)),
            new (new Vector3(8, 8, 8), new Vector3(9, 9, 9)),
            new (new Vector3(9, 9, 9), new Vector3(10, 10, 10)),
        };
        
        [Test]
        public void HandleTap_ShouldCallRayProvider_WithVectorFromHandleTouchPosition([ValueSource(nameof(TouchPositions))]Vector2 touchPosition)
        {
            var rayProviderMock = new Mock<IRayProvider>();
            rayProviderMock.Setup(r => r.ScreenPointToRay(It.IsAny<Vector2>())).Returns(new Ray());
            var raycasterMock = new Mock<IRaycaster>();
            raycasterMock.Setup(r => r.GetRayIntersections<IClickable>(It.IsAny<Ray>())).Returns(new List<IClickable>());
            var handler = new TouchInputHandler(rayProviderMock.Object, raycasterMock.Object);
            
            handler.HandleTouchPosition(touchPosition);
            handler.HandleTap();
            
            rayProviderMock.Verify(r => r.ScreenPointToRay(touchPosition), Times.Once);
        }

        [Test]
        public void HandleTap_ShouldPassRayFromRayProvider_ToGivenRaycaster([ValueSource(nameof(Rays))]Ray ray)
        {
            var rayProviderMock = new Mock<IRayProvider>();
            rayProviderMock.Setup(r => r.ScreenPointToRay(It.IsAny<Vector2>())).Returns(ray);
            var raycasterMock = new Mock<IRaycaster>();
            raycasterMock.Setup(r => r.GetRayIntersections<IClickable>(It.IsAny<Ray>())).Returns(new List<IClickable>());
            var handler = new TouchInputHandler(rayProviderMock.Object, raycasterMock.Object);
            
            handler.HandleTouchPosition(new Vector2());
            handler.HandleTap();
            
            raycasterMock.Verify(r => r.GetRayIntersections<IClickable>(ray), Times.Once);
        }

        [Test]
        public void HandleTap_ShouldCallClickMethod_OnAllClickablesFromRaycasterResult()
        {
            var clickableMocks = new List<Mock<IClickable>>
            {
                new(), new(), new(), new(), new()
            };
            var clickables = new List<IClickable>();
            clickableMocks.ForEach(c => clickables.Add(c.Object));
            var rayProviderMock = new Mock<IRayProvider>();
            rayProviderMock.Setup(r => r.ScreenPointToRay(It.IsAny<Vector2>())).Returns(new Ray());
            var raycasterMock = new Mock<IRaycaster>();
            raycasterMock.Setup(r => r.GetRayIntersections<IClickable>(It.IsAny<Ray>())).Returns(clickables);
            var handler = new TouchInputHandler(rayProviderMock.Object, raycasterMock.Object);
            
            handler.HandleTouchPosition(new Vector2());
            handler.HandleTap();
            
            clickableMocks.ForEach(c => c.Verify(cl => cl.Click(), Times.Once));
        }
    }
}