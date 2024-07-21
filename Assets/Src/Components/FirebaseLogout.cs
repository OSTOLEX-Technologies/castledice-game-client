using System.Collections.Generic;
using Firebase.Auth;
using Src.Analytics.Events;
using Src.Analytics.Identity;
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
        
        [SerializeField] private SceneLoader sceneLoader;
        
        private IAuthTokenSaver _saver;
        private IDateTimeRetriever _dateTimeRetriever;
        private IBranchLogout _branchLogout;

        public void Init(
            IAuthTokenSaver saver,
            IDateTimeRetriever dateTimeRetriever,
            IBranchLogout branchLogout)
        {
            _saver = saver;
            _dateTimeRetriever = dateTimeRetriever;
            _branchLogout = branchLogout;
        }

        public void Logout()
        {
            if (Singleton<IAccessTokenProvider>.Registered)
            {
                Singleton<IAccessTokenProvider>.Unregister();
            }
            _saver.DeleteAuthTokens();
            sceneLoader.LoadSceneWithTransition(SceneType.Auth);
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
            Debug.Log("Log out");
            
            _branchLogout.Logout();
        }
    }
}
