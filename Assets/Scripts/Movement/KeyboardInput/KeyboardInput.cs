using Movement;
using Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private bool sendUpdates;
    [SerializeField] private NetworkSender sender;
    [SerializeField] private Body body;
    [SerializeField] private float movementSPeed;
    [SerializeField] private float crouchSpeed;

    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    [SerializeField] private KeyCode jumpKey;

    private void Update()
    {
        if (Input.GetKey(leftKey))
            OnMoveLeft();

        if (Input.GetKey(rightKey))
            OnMoveRight();

        if (Input.GetKey(upKey))
            CrouchUp();

        if (Input.GetKey(downKey))
            CrouchDown();

        if (Input.GetKeyDown(jumpKey))
            Jump();

        if (Input.GetKeyUp(leftKey) || Input.GetKeyUp(rightKey))
            CancelMovement();

        if (Input.GetKeyUp(upKey) || Input.GetKeyUp(downKey))
            CancelCrouch();

    }

    public void Jump()
    {
        body.Jump(0f);
        if (sendUpdates)
            sender?.Send("OnJumpUpdate", string.Empty);
    }

    public void CrouchDown()
    {
        body.Crouch(-crouchSpeed * Time.deltaTime);
        if (sendUpdates)
            sender?.Send("OnCrouchDown", string.Empty);
    }

    public void CrouchUp()
    {
        body.Crouch(crouchSpeed * Time.deltaTime);
        if (sendUpdates)
            sender?.Send("OnCrouchUp", string.Empty);
    }

    public void CancelCrouch()
    {
        if (sendUpdates)
            sender?.Send("StopCrouch", string.Empty);
    }

    public void OnMoveLeft()
    {
        body.Move(movementSPeed * Time.deltaTime);
        if (sendUpdates)
            sender?.Send("OnMoveLeft", string.Empty);
    }

    public void OnMoveRight()
    {
        body.Move(-movementSPeed * Time.deltaTime);
        if (sendUpdates)
            sender?.Send("OnMoveRight", string.Empty);
    }

    public void CancelMovement()
    {
        if (sendUpdates)
            sender?.Send("StopMovement", string.Empty);
    }
}
