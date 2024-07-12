using Firebase;
using Firebase.Auth;
using Src.Auth.AuthTokenSaver;
using Src.Auth.TokenProviders;
using Src.General.Caching;
using Src.General.SceneTransitionCommands;
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
            Singleton<IAccessTokenProvider>.Unregister();
            FirebaseAuth.DefaultInstance.SignOut();
            _saver.DeleteAuthTokens();
            _authSceneTransitionHandler.HandleTransitionCommand();
        }
    }
}
