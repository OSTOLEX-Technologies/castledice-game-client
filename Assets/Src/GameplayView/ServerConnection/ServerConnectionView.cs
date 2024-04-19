using Riptide;
using TMPro;
using UnityEngine;

namespace Src.GameplayView.ServerConnection
{
    public class ServerConnectionView : IServerConnectionView
    {
        private readonly GameObject _connectingMessage;
        private readonly GameObject _connectionFailedMessage;
        private readonly TextMeshProUGUI _connectionFailedReasonText;
        private readonly IRejectReasonMessagesConfig _rejectReasonMessagesConfig;

        public ServerConnectionView(GameObject connectingMessage, GameObject connectionFailedMessage, TextMeshProUGUI connectionFailedReasonText, IRejectReasonMessagesConfig rejectReasonMessagesConfig)
        {
            _connectingMessage = connectingMessage;
            _connectionFailedMessage = connectionFailedMessage;
            _connectionFailedReasonText = connectionFailedReasonText;
            _rejectReasonMessagesConfig = rejectReasonMessagesConfig;
        }

        public void ShowConnectingMessage()
        {
            _connectingMessage.SetActive(true);
        }

        public void HideConnectingMessage()
        {
            _connectingMessage.SetActive(false);
        }

        public void ShowConnectionFailedMessage(RejectReason reason)
        {
            _connectionFailedMessage.SetActive(true);
            _connectionFailedReasonText.text = _rejectReasonMessagesConfig.GetRejectReasonMessage(reason);
        }
    }
}