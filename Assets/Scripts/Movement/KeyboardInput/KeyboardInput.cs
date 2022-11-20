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
            sender?.Send("OnJumpUpdate", "jump");
    }

    public void CrouchDown()
    {
        body.Crouch(-crouchSpeed * Time.deltaTime);
        if (sendUpdates)
            sender?.Send("OnCrouchDown", "c_down");
    }

    public void CrouchUp()
    {
        body.Crouch(crouchSpeed * Time.deltaTime);
        if (sendUpdates)
            sender?.Send("OnCrouchUp", "c_up");
    }

    public void CancelCrouch()
    {
        if (sendUpdates)
            sender?.Send("StopCrouch", "c_c");
    }

    public void OnMoveLeft()
    {
        body.Move(movementSPeed * Time.deltaTime);
        if (sendUpdates)
            sender?.Send("OnMoveLeft", "m_l");
    }

    public void OnMoveRight()
    {
        body.Move(-movementSPeed * Time.deltaTime);
        if (sendUpdates)
            sender?.Send("OnMoveRight", "m_r");
    }

    public void CancelMovement()
    {
        if (sendUpdates)
            sender?.Send("StopMovement", "c_m");
    }
}
