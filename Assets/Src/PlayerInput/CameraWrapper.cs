using UnityEngine;

namespace Src.PlayerInput
{
    public class CameraWrapper : IRayProvider
    {
        private readonly Camera _camera;

        public CameraWrapper(Camera camera)
        {
            _camera = camera;
        }

        public Ray ScreenPointToRay(Vector2 screenPoint)
        {
            return _camera.ScreenPointToRay(screenPoint);
        }
    }
}