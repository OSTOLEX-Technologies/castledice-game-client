using Src.GameplayView.ClickDetection;
using UnityEngine;

namespace Src.PlayerInput
{
    public class TouchInputHandler : ITouchInputHandler
    {
        private Vector2 _currentTouchPosition;
        private readonly IRayProvider _rayProvider;
        private readonly IRaycaster _raycaster;

        public TouchInputHandler(IRayProvider rayProvider, IRaycaster raycaster)
        {
            _rayProvider = rayProvider;
            _raycaster = raycaster;
        }

        public void HandleTouchPosition(Vector2 position)
        {
            _currentTouchPosition = position;
        }

        public void HandleTap()
        {
            var ray = _rayProvider.ScreenPointToRay(_currentTouchPosition);
            var rayIntersections = _raycaster.GetRayIntersections<IClickable>(ray);
            foreach (var rayIntersection in rayIntersections)
            {
                rayIntersection.Click();
            }
        }
    }
}