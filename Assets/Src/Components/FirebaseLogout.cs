using Src.Auth.AuthTokenSaver;
using Src.SceneTransitionCommands;
using UnityEngine;

namespace Src.Components
{
    public class FirebaseLogout : MonoBehaviour
    {
        private IAuthTokenSaver _saver;
        private ISceneTransitionHandler _authSceneTransitionHandler;

        public void Init(
            IAuthTokenSaver saver,
            ISceneTransitionHandler authSceneTransitionHandler)
        {
            _saver = saver;
            _authSceneTransitionHandler = authSceneTransitionHandler;
        }
        
        public void Logout()
        {
            _saver.DeleteAuthTokens();
            _authSceneTransitionHandler.HandleTransitionCommand();
        }
    }
}
