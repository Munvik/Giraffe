using Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tests
{
    public class VRHeadTest : MonoBehaviour
    {
        [SerializeField] private NetworkSender sender;
        [SerializeField] private Transform head;

        [SerializeField] private Vector3 position;
        [SerializeField] private Vector3 rotation;

        [SerializeField] private TMP_Text positionText;
        [SerializeField] private TMP_Text rotationText;

        private void Start()
        {
            StartCoroutine(SendCoroutine());
        }

        private void SendUpdate()
        {
            HeadData headData = new HeadData(head.position, head.rotation.eulerAngles);
            position = head.position;
            rotation = head.rotation.eulerAngles;
            positionText.text = position.ToString();
            rotationText.text = rotation.ToString();

            sender.Send("OnHeadUpdate", JsonUtility.ToJson(headData));
        }

        public float timeSpan;

        IEnumerator SendCoroutine()
        {
            yield return new WaitForSeconds(timeSpan);
            SendUpdate();
            StartCoroutine(SendCoroutine());    
        }
    }
}
