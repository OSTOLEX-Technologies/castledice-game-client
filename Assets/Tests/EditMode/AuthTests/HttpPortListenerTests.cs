using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using Proyecto26;
using Src.AuthController;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Responses;
using UnityEngine;
using UnityEngine.Networking;

namespace Tests.EditMode.AuthTests
{
    public class HttpPortListenerTests
    {
        private HttpPortListener _listener;
        private const int Port = 1234;

        [SetUp]
        public void SetUp()
        {
            _listener = new HttpPortListener(Port);
        }
        
        [Test]
        public async Task StartListening_ShouldFireCallback()
        {
            string expectedCode = "someComplexCode";
            string resultCode = "";
            
            void Callback(string code)
            {
                Debug.Log(code);
                resultCode = code;

                _listener.StopListening();
            }

            //UnityMainThreadDispatcher instancing
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var component = go.AddComponent<UnityMainThreadDispatcher>();
         
            RestClient.Request(new RequestHelper
            {
                Method = "POST",
                Uri = $"http://localhost:{Port}",
                Params = new Dictionary<string, string>
                {
                    { "code", expectedCode },
                    { "client_id", "ClientId" },
                    { "client_secret", "ClientSecret" },
                    { "redirect_uri", "RedirectUri" },
                    { "grant_type", "authorization_code" }
                }
            });
            _listener.StartListening(Callback);
            
            await Task.Delay(300);
            
            Assert.AreEqual(expectedCode, resultCode);

            GameObject.Destroy(go.gameObject);
        }
    }
}