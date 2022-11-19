using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LobbyCreator;
using UnityEngine.Events;

namespace Networking
{
    public class NetworkReceiver : MonoBehaviour
    {
        [SerializeField] private Lobby lobby;
        public string currentPayload;

        private void Start()
        {

        }

        public void Receive(string data)
        {
            Debug.Log("Received data = " + data);
            NetworkPayloadData payloadData = JsonUtility.FromJson<NetworkPayloadData>(data);
            currentPayload = payloadData.payload;

            Invoke(payloadData.methodName, 0f);
        }

        private void OnUserConnected()
        {
            ClientData clientData = JsonUtility.FromJson<ClientData>(currentPayload);
            lobby.Join(clientData);
        }

        public void OnBodyMoved()
        {
            BodyData bodyData = JsonUtility.FromJson<BodyData>(currentPayload);
        }

        public void OnHeadMoved()
        {
            HeadData headData = JsonUtility.FromJson<HeadData>(currentPayload);
        }

        public void OnGameStarted()
        {

        }
    }

    [System.Serializable]
    public class BodyData
    {
        public Vector3 position;
    }
    
    [System.Serializable]
    public class HeadData
    {
        public Vector3 position;
    }
}
