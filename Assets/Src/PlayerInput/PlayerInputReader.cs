using UnityEngine;
using UnityEngine.InputSystem;

namespace Src.PlayerInput
{
    public class PlayerInputReader
    {
        private readonly PlayerInputActions _playerInputActions;
        private readonly PlayerInputActions.GameplayActions _gameplayActions;
        private readonly ITouchInputHandler _touchInputHandler;

        public PlayerInputReader(ITouchInputHandler touchInputHandler)
        {
            _playerInputActions = new PlayerInputActions();
            _gameplayActions = _playerInputActions.Gameplay;
            _touchInputHandler = touchInputHandler;
            
            _gameplayActions.TouchPosition.performed += ProcessTouchPosition;
            _gameplayActions.Tap.performed +=  ProcessTap;
        }

        private void ProcessTap(InputAction.CallbackContext ctx)
        {
            _touchInputHandler.HandleTap();
        }

        private void ProcessTouchPosition(InputAction.CallbackContext ctx)
        {
            _touchInputHandler.HandleTouchPosition(ctx.ReadValue<Vector2>());
        }

        public void Enable()
        {
            _playerInputActions.Enable();
        }

        public void Disable()
        {
            _playerInputActions.Disable();
        }
    }
}