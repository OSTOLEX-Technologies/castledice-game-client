using Src.Auth.AuthTokenSaver;
using Src.SceneTransitionCommands;
using TMPro;
using UnityEngine;

namespace Src.Auth.Scripts
{
    public class AuthSceneFlow : MonoBehaviour
    {
        private const string InitializingLabelText = "Initializing...";
        private const string LoadingLabelText = "Loading...";
        private const string ImmediateLoginFromSavedTokenLabelText = "Logging in...";

        [SerializeField, InspectorName("Processing Canvas")]
        private Canvas processingCanvas;
        [SerializeField, InspectorName("Auth Canvas")]
        private Canvas authCanvas;

        [SerializeField, InspectorName("Processing Canvas Label")]
        private TextMeshProUGUI processingCanvasLabel;
        
        private IAuthView _authView;
        private IAuthTokenSaver _authTokenSaver;
        private AuthSceneTransitionHandler _sceneTransitionHandler;

        private bool _flowInProgress;

        private void Start()
        {
            if (_flowInProgress) return;

            authCanvas.gameObject.SetActive(false);
            processingCanvas.gameObject.SetActive(true);
            processingCanvasLabel.SetText(InitializingLabelText);
        }

        public void StartSceneFlow(
            IAuthView authView,
            IAuthTokenSaver authTokenSaver,
            AuthSceneTransitionHandler sceneTransitionHandler)
        {
            _flowInProgress = true;
            
            _authView = authView;
            _authTokenSaver = authTokenSaver;
            _sceneTransitionHandler = sceneTransitionHandler;
            
            processingCanvasLabel.SetText(LoadingLabelText);
            SubscribeOnAuthCompleted();

            if (_authTokenSaver.TryGetLastAuthType(out var authType))
            {
                processingCanvasLabel.SetText(ImmediateLoginFromSavedTokenLabelText);
                processingCanvas.gameObject.SetActive(true);
                
                _authView.HideAuthUI();
                _authView.Login(authType);
            }
            else
            {
                processingCanvas.gameObject.SetActive(false);
                authCanvas.gameObject.SetActive(true);
            }
        }
        
        private void SubscribeOnAuthCompleted()
        {
            _authView.AuthCompleted += () =>
            {
                _sceneTransitionHandler.HandleTransitionCommand();
                _authView.AuthCompleted -= SubscribeOnAuthCompleted;
            };
        }
    }
}