using UnityEngine;

namespace Src.NetworkingModule.ConnectionConfiguration
{
    [CreateAssetMenu(fileName = "GameServerConnectionConfig", menuName = "Configs/GameServerConnectionConfig", order = 3)]

    public class GameServerConnectionConfig : ScriptableObject
    {
        [SerializeField] private string ip;
        [SerializeField] private ushort port;
        
        public string Ip => ip;
        public ushort Port => port;
    }
}