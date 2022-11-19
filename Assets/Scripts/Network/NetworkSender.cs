using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

namespace Networking
{
    public class NetworkSender : MonoBehaviour, INetworkSender
    {
        [SerializeField] private WebSocketConnector connector;

        public void Send(string methodName, string data)
        {
            connector.Send(methodName, data);
        }

        public void StartGame()
        {
            Send("OnGameStarted", "");
        }
    }
}
