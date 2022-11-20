using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LobbyCreator;
using UnityEngine.Events;
using Movement;

namespace Networking
{
    public class NetworkReceiver : MonoBehaviour
    {
        [SerializeField] private Lobby lobby;
        [SerializeField] private KeyboardInput keyInput;
        [SerializeField] private WebSocketConnector connector;
        public string currentPayload;

        public bool crouchDown;
        public bool crouchUp;
        public bool updateCrouch;

        public bool moveLeft;
        public bool moveRight;
        public bool updateMovement;

        private void Start()
        {

        }

        private void Update()
        {
            if (updateMovement)
            {
                if(moveLeft)
                    keyInput.OnMoveLeft();

                if(moveRight)
                    keyInput.OnMoveRight();
            }

            if (updateCrouch)
            {
                if (crouchDown)
                    keyInput.CrouchDown();

                if (crouchUp)
                    keyInput.CrouchUp();
            }
        }

        public void Receive(string data)
        {
            NetworkPayloadData payloadData = JsonUtility.FromJson<NetworkPayloadData>(data);
            currentPayload = payloadData.payload;
            Debug.Log("Received data = " + data);
            Invoke(payloadData.methodName, 0f);
        }

        private void OnUserConnected()
        {
            ClientData clientData = JsonUtility.FromJson<ClientData>(currentPayload);
            lobby?.Join(clientData);
        }

        public void OnBodyUpdate()
        {
            BodyData bodyData = JsonUtility.FromJson<BodyData>(currentPayload);
        }

        public HeadData data;
        public Transform target;

        public void OnHeadUpdate()
        {
            HeadData headData = JsonUtility.FromJson<HeadData>(currentPayload);
            data = headData;

            target.transform.position = data.position;
            target.transform.rotation = Quaternion.Euler(data.rotation);
        }

        public void OnCrouchDown()
        {
            updateCrouch = true;
            crouchDown = true;
        }

        public void OnCrouchUp()
        {
            updateCrouch = true;
            crouchUp = true;
        }

        public void StopCrouch()
        {
            updateCrouch = false;
            crouchDown = false;
            crouchUp = false;
        }

        public void OnMoveLeft()
        {
            updateMovement = true;
            moveLeft = true;
        }

        public void OnMoveRight()
        {
            updateMovement = true;
            moveRight = true;
        }

        public void StopMovement()
        {
            updateMovement = false;
            moveRight = false;
            moveLeft = false;
        }

        public void OnJumpUpdate()
        {
            //FloatData data = JsonUtility.FromJson<FloatData>(currentPayload);
            keyInput.Jump();
        }

        public void OnMoveUpdate()
        {
            //FloatData data = JsonUtility.FromJson<FloatData>(currentPayload);
            //body.targetMovement = data.floatVal;
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

    [System.Serializable]
    public class FloatData
    {
        public float floatVal;
    }

    [System.Serializable]
    public class BoolData
    {
        public bool boolVal;
    }
}
