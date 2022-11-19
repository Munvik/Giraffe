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

        private void Update()
        {
            HeadData headData = new HeadData(head.localPosition, head.localRotation.eulerAngles);
            position = head.localPosition;
            rotation = head.localRotation.eulerAngles;

            sender.Send("OnHeadUpdate", JsonUtility.ToJson(headData));
        }
    }
}
