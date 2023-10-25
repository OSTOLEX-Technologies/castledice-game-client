using System.Numerics;

namespace Src.PlayerInput
{
    public interface ITouchInputHandler
    {
        void HandleTouchPosition(Vector2 position);
        void HandleTap();
    }
}