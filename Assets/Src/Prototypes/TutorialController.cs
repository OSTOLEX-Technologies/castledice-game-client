using System.Collections.Generic;
using Src.PlayerInput;
using UnityEngine;

namespace Src.Prototypes
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> frames;
        [SerializeField] private List<int> framesToSwitchByClick;
        [SerializeField] private List<int> framesToSwitchByRightMove;
        [SerializeField] private List<int> framesWhereRaycastShouldBeBlocked;
        private int _currentFrameIndex;
        private GameObject _currentFrame;
        private BlockableRaycaster3D _raycaster;
        
        public void Init(BlockableRaycaster3D raycaster)
        {
            _raycaster = raycaster;
        }

        private void Start()
        {
            SwitchToFrame(0);
        }

        public void ScreenClicked()
        {
            Debug.Log("Screen clicked");
            if (!framesToSwitchByClick.Contains(_currentFrameIndex)) return;
            SwitchToNextFrame();
        }

        public void RightMoveApplied()
        {
            if (!framesToSwitchByRightMove.Contains(_currentFrameIndex)) return;
            SwitchToNextFrame();
        }

        private void SwitchToNextFrame()
        {
            _currentFrameIndex++;
            SwitchToFrame(_currentFrameIndex);
        }
        
        private void SwitchToFrame(int index)
        {
            _currentFrame?.SetActive(false);
            _currentFrame = frames[index];
            _currentFrame.SetActive(true);
            if (framesWhereRaycastShouldBeBlocked.Contains(index))
            {
                _raycaster.Block();
            }
            else
            {
                _raycaster.Unblock();
            }
        }

        public void WrongMoveApplied()
        {
        
        }
    }
}
