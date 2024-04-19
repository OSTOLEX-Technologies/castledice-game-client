
using Src.GameplayPresenter.GameCreation.GameSearching;
using UnityEngine;

namespace Src.GameplayPresenter.GameCreation
{
    [CreateAssetMenu(fileName = "GameSearchFailMessagesConfig", menuName = "Configs/GameSearchFailMessagesConfig")]

    public class FailMessagesConfig : ScriptableObject, IFailMessagesConfig
    {
        [SerializeField] private string notInitializedMessage;
        [SerializeField] private string notConnectedMessage;
        
        public string GetFailMessage(SearchFailReason reason)
        {
            return reason switch
            {
                SearchFailReason.NotInitialized => notInitializedMessage,
                SearchFailReason.NotConnected => notConnectedMessage,
                _ => "Unknown error"
            };
        }
    }
}