using Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{
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
            body.Move(movementSPeed * Time.deltaTime);

        if (Input.GetKey(rightKey))
            body.Move(-movementSPeed * Time.deltaTime);

        if (Input.GetKey(upKey))
            body.Crouch(crouchSpeed * Time.deltaTime);

        if (Input.GetKey(downKey))
            body.Crouch(-crouchSpeed * Time.deltaTime);

        if (Input.GetKeyDown(jumpKey))
            body.Jump(0f);
    }
}
