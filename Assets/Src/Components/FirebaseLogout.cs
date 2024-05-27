using Firebase.Auth;
using Src.Auth.AuthTokenSaver;
using Src.Auth.TokenProviders;
using Src.General.Caching;
using Src.General.LoadingScenes;
using UnityEngine;

namespace Src.Components
{
    public class FirebaseLogout : MonoBehaviour
    {
        private IAuthTokenSaver _saver;
        private SceneLoader _sceneLoader;

        public void Init(
            IAuthTokenSaver saver,
            SceneLoader sceneLoader)
        {
            _saver = saver;
            _sceneLoader = sceneLoader;
        }

        public void Logout()
        {
            Singleton<IAccessTokenProvider>.Unregister();
            _saver.DeleteAuthTokens();
            _sceneLoader.LoadSceneWithTransition(SceneType.Auth);
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }
}
