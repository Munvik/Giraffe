using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Camera cam;
    public float zoomFrom;
    public float zoomTo;
    public float moveSpeed;

    public Vector3 rotationFrom;
    public Vector3 rotationTo;
    public float rotationSpeed;

    private void Update()
    {
        float movementValue = Mathf.InverseLerp(-1f, 1f, Mathf.Sin(Time.time * moveSpeed));
        float rotationValue = Mathf.InverseLerp(-1f, 1f, Mathf.Sin(Time.time * rotationSpeed));

        cam.orthographicSize = Mathf.Lerp(zoomFrom, zoomTo, movementValue);
        cam.transform.localRotation = Quaternion.Euler(Vector3.Lerp(rotationFrom, rotationTo, rotationValue));
    }

}
