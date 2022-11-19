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

        private void Start()
        {

        }

        public void Receive(string data)
        {
            Debug.Log("Received data = " + data);
            NetworkPayloadData payloadData = JsonUtility.FromJson<NetworkPayloadData>(data);
        }

        public void OnBodyMoved(string data)
        {
            BodyData bodyData = JsonUtility.FromJson<BodyData>(data);
        }

        public void OnHeadMoved(string data)
        {
            HeadData headData = JsonUtility.FromJson<HeadData>(data);
        }

        public void OnGameStarted(string data)
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
