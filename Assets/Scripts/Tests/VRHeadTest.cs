using Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class VRHeadTest : MonoBehaviour
    {
        [SerializeField] private NetworkSender sender;
        [SerializeField] private Transform head;

        [SerializeField] private Vector3 position;
        [SerializeField] private Vector3 rotation;

        private void Start()
        {
            StartCoroutine(SendCoroutine());
        }

        private void SendUpdate()
        {
            HeadData headData = new HeadData(head.position, head.rotation.eulerAngles);
            position = head.position;
            rotation = head.rotation.eulerAngles;

            sender.Send("OnHeadUpdate", JsonUtility.ToJson(headData));
        }

        IEnumerator SendCoroutine()
        {
            SendUpdate();
            yield return 0.5f;
            StartCoroutine(SendCoroutine());    
        }
    }
}
