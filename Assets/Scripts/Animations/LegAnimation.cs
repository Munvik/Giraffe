using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 from;
    [SerializeField] private Vector3 to;

    public void UpdateRot(float value)
    {
        transform.localRotation = Quaternion.Euler(Vector3.Lerp(from, to, value));
    }
}
