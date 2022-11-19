using LobbyCreator;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Networking
{
    public class WebSocketConnector : MonoBehaviour
    {
        [SerializeField] private string ip;
        [SerializeField] private int port;
        private Socket client;

        [SerializeField] private NetworkReceiver networkReceiver;

        public ClientData myClientData;

        [Header("Test data")]
        public float rateTest;
        public float currentTime = 0f;
        public bool testRating = false;

        private void Start()
        {
            ConnectToServer(ip, port);
        }

        private void Update()
        {
            TryReceive();

            if (!testRating)
                return;

            currentTime += Time.deltaTime;
            if (currentTime >= rateTest)
            {
                Send("Test method", "Test hello... Wanna cofee?");
                currentTime = 0f;
            }
        }

        private void ConnectToServer(string ip, int port)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(ip, port);
            if (!client.Connected)
            {
                Debug.LogError("Connection Failed");
            }

            Send("OnUserConnected", JsonUtility.ToJson(myClientData));
        }

        public void Send(string methodName, string message)
        {           
            if(!client.Connected)
            {
                Debug.LogError("No connect to server");
                return;
            }

            NetworkPayloadData networkData = ComposeData(methodName, message);
            byte[] bytesMessage = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(networkData));
            client.Send(bytesMessage);
        }

        public NetworkPayloadData ComposeData(string methodName, string payload)
        {
            NetworkPayloadData data = new NetworkPayloadData(methodName, payload, myClientData);
            return data;
        }

        private void TryReceive()
        {
            if (client.Available == 0)
                return;

            //allocate and receive bytes
            byte[] bytes = new byte[256];
            int idxUsedBytes = client.Receive(bytes);
            if (idxUsedBytes == 0)
                return;

            string receivedData = Encoding.UTF8.GetString(bytes);
            Receive(receivedData);
        }

        private void Receive(string message)
        {
            networkReceiver.Receive(message);
        }

        private void OnApplicationQuit()
        {
            Abort();
        }

        public void Abort()
        {
            client.Disconnect(false);
        }

        #region DEVELOPMENT REGION
        public string testMessage;

        [ContextMenu("Send test message to server")]
        public void SendTestMessage()
        {
            Send("No Method :)", testMessage);
        }
        #endregion
    }

    [System.Serializable]
    public class NetworkPayloadData
    {
        public string methodName;
        public string payload;
        public ClientData clientData;

        public NetworkPayloadData(string methodName, string payload, ClientData clientData)
        {
            this.methodName = methodName;
            this.payload = payload;
            this.clientData = clientData;
        }
    }
}
