using System.Collections.Generic;
using Firebase.Auth;
using Src.Analytics.Events;
using Src.Auth.AuthTokenSaver;
using Src.Auth.TokenProviders;
using Src.General.Caching;
using Src.General.LoadingScenes;
using Src.General.TimeRetriever;
using UnityEngine;

namespace Src.Components
{
    public class FirebaseLogout : MonoBehaviour
    {
        private const string LogoutTimestampParamName = "Timestamp";
        
        private IAuthTokenSaver _saver;
        private SceneLoader _sceneLoader;
        private IDateTimeRetriever _dateTimeRetriever;

        public void Init(
            IAuthTokenSaver saver,
            SceneLoader sceneLoader,
            IDateTimeRetriever dateTimeRetriever)
        {
            _saver = saver;
            _sceneLoader = sceneLoader;
            _dateTimeRetriever = dateTimeRetriever;
        }

        public void Logout()
        {
            Singleton<IAccessTokenProvider>.Unregister();
            _saver.DeleteAuthTokens();
            _sceneLoader.LoadSceneWithTransition(SceneType.Auth);
            FirebaseAuth.DefaultInstance.SignOut();

            var branchEventParams = new Dictionary<string, string>
            {
                {
                    LogoutTimestampParamName, 
                    _dateTimeRetriever.GetFormattedDateTime()
                }
            };
            BranchEventSender.SendCustomEvent(
                BranchEventNames.Logout, 
                branchEventParams);
        }
    }
}
