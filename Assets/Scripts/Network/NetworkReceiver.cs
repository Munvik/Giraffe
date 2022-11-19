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

        public void OnBodyUpdate()
        {
            BodyData bodyData = JsonUtility.FromJson<BodyData>(currentPayload);
        }

        public HeadData data;

        public void OnHeadUpdate()
        {
            HeadData headData = JsonUtility.FromJson<HeadData>(currentPayload);
            data = headData;
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
        public Vector3 rotation;

        public HeadData(Vector3 position, Vector3 rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
}
