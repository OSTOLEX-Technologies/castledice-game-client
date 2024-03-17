using Firebase.Messaging;
using UnityEngine;

namespace Src.PushNotifications
{
    public class PushNotificationsHandler : MonoBehaviour
    {
        private void Start() {
            FirebaseMessaging.TokenReceived += OnTokenReceived;
            FirebaseMessaging.MessageReceived += OnMessageReceived;
            
            DontDestroyOnLoad(gameObject);
        }

        private static void OnTokenReceived(object sender, TokenReceivedEventArgs token) {
            Debug.Log("Received Registration Token: " + token.Token);
            //TODO: implement token caching 
        }

        private static void OnMessageReceived(object sender, MessageReceivedEventArgs e) {
            Debug.Log("Received a new message from: " + e.Message.From);
        }
    }
}