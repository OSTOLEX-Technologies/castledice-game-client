using System.Collections.Generic;
using UnityEngine;

namespace MetaMask.Plugins.Libraries.SocketIOUnity.Runtime
{

    public class WebSocketDispatcher : MonoBehaviour
    {

        private static WebSocketDispatcher instance;

        public static WebSocketDispatcher Instance
        {
            get
            {
                return instance;
            }
        }

        private List<WebSocket.WebSocket> webSockets = new List<WebSocket.WebSocket>();

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        private void Update()
        {
            DispatchMessageQueue();
        }

        public void DispatchMessageQueue()
        {
            for (int i = 0; i < this.webSockets.Count; i++)
            {
                this.webSockets[i].DispatchMessageQueue();
            }
        }
#endif

        public void AddWebSocket(WebSocket.WebSocket webSocket)
        {
            this.webSockets.Add(webSocket);
        }

        public void RemoveWebSocket(WebSocket.WebSocket webSocket)
        {
            this.webSockets.Remove(webSocket);
        }

    }

}