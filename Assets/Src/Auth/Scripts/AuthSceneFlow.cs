using Src.Auth.AuthTokenSaver;
using Src.SceneTransitionCommands;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Auth.Scripts
{
    public class AuthSceneFlow : MonoBehaviour
    {
        private const string InitializingLabelText = "Initializing...";
        private const string LoadingLabelText = "Loading...";
        private const string ImmediateLoginFromSavedTokenLabelText = "Logging in...";

        [SerializeField, InspectorName("Processing Canvas")]
        private Canvas processingCanvas;

        [SerializeField, InspectorName("Auth Buttons")] 
        private Button[] authButtons;

        [SerializeField, InspectorName("Processing Canvas Label")]
        private TextMeshProUGUI processingCanvasLabel;
        
        private IAuthView _authView;
        private IAuthTokenSaver _authTokenSaver;
        private SceneTransitionHandler _sceneTransitionHandler;

        private bool _bFlowInProgress;

        private void Start()
        {
            if (_bFlowInProgress) return;
            
            ControlAuthButtons(false);
            
            processingCanvas.gameObject.SetActive(true);
            processingCanvasLabel.SetText(InitializingLabelText);
        }

        public void StartSceneFlow(
            IAuthView authView,
            IAuthTokenSaver authTokenSaver,
            SceneTransitionHandler sceneTransitionHandler)
        {
            _bFlowInProgress = true;
            
            _authView = authView;
            _authTokenSaver = authTokenSaver;
            _sceneTransitionHandler = sceneTransitionHandler;

            processingCanvasLabel.SetText(LoadingLabelText);
            SubscribeOnAuthCompleted();

            if (_authTokenSaver.TryGetLastLoginInfo(out var authType))
            {
                processingCanvasLabel.SetText(ImmediateLoginFromSavedTokenLabelText);
                processingCanvas.gameObject.SetActive(true);
                
                ControlAuthButtons(false);
                _authView.Login(authType);
            }
            else
            {
                ControlAuthButtons(true);
                processingCanvas.gameObject.SetActive(false);
            }
        }
        
        private void SubscribeOnAuthCompleted()
        {
            _authView.AuthCompleted += () =>
            {
                ControlAuthButtons(false);
                _sceneTransitionHandler.HandleTransitionCommand();
                _authView.AuthCompleted -= SubscribeOnAuthCompleted;
            };
        }

        private void ControlAuthButtons(bool bEnable)
        {
            foreach (var button in authButtons)
            {
                button.interactable = bEnable;
            }
        }
    }
}