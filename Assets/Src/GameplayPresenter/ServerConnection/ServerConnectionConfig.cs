using UnityEngine;

namespace Src.GameplayPresenter.ServerConnection
{
    [CreateAssetMenu(fileName = "ServerConnectionConfig", menuName = "Configs/ServerConnectionConfig")]
    public class ServerConnectionConfig : ScriptableObject, IServerConnectionConfig 
    {
        [SerializeField] private string hostAddress;
        [SerializeField] private int maxConnectionAttempts;

        public string HostAddress => hostAddress;

        public int MaxConnectionAttempts => maxConnectionAttempts;
    }
}