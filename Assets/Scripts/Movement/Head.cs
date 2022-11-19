using Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour, IHead
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    public bool testMode;
    public Vector3 targetPosition;
    public Vector3 targetRotation;

    private void Update()
    {
        if (testMode)
        {
            ApplyPosition(targetPosition);
            ApplyRotation(targetRotation);
        }

        UpdatePosition();
        UpdateRotation();
    }

    public void ApplyPosition(Vector3 position)
    {
        transform.localPosition = position;        
    }

    public void ApplyRotation(Vector3 rotation)
    {
        transform.localRotation = Quaternion.Euler(rotation);
    }

    private void UpdatePosition()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void UpdateRotation()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotateSpeed);
    }
}
