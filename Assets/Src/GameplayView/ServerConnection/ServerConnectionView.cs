using Riptide;
using TMPro;
using UnityEngine;

namespace Src.GameplayView.ServerConnection
{
    public class ServerConnectionView : IServerConnectionView
    {
        private readonly GameObject _connectionMessage;
        private readonly GameObject _connectionFailedMessage;
        private readonly TextMeshProUGUI _connectionFailedReasonText;
        private readonly IRejectReasonMessagesConfig _rejectReasonMessagesConfig;

        public ServerConnectionView(GameObject connectionMessage, GameObject connectionFailedMessage, TextMeshProUGUI connectionFailedReasonText, IRejectReasonMessagesConfig rejectReasonMessagesConfig)
        {
            _connectionMessage = connectionMessage;
            _connectionFailedMessage = connectionFailedMessage;
            _connectionFailedReasonText = connectionFailedReasonText;
            _rejectReasonMessagesConfig = rejectReasonMessagesConfig;
        }

        public void ShowConnectingMessage()
        {
            throw new System.NotImplementedException();
        }

        public void HideConnectingMessage()
        {
            throw new System.NotImplementedException();
        }

        public void ShowConnectionFailedMessage(RejectReason reason)
        {
            throw new System.NotImplementedException();
        }
    }
}