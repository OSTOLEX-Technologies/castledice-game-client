using Riptide;
using UnityEngine;

namespace Src.GameplayView.ServerConnection
{
    public class RejectReasonMessagesConfig : ScriptableObject, IRejectReasonMessagesConfig
    {
        [SerializeField] private string noConnection;
        [SerializeField] private string alreadyConnected;
        [SerializeField] private string serverFull;
        [SerializeField] private string rejected;
        [SerializeField] private string custom;
        
        public string GetRejectReasonMessage(RejectReason reason)
        {
            return reason switch
            {
                RejectReason.NoConnection => noConnection,
                RejectReason.AlreadyConnected => alreadyConnected,
                RejectReason.ServerFull => serverFull,
                RejectReason.Rejected => rejected,
                RejectReason.Custom => custom,
                _ => string.Empty
            };
        }
    }
}