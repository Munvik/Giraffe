using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadOverider : MonoBehaviour
{
    [SerializeField] private Head head;
    [SerializeField] private Transform applyFrom;

    [SerializeField] private bool followCamera = false;

    private void Update()
    {
        if (!followCamera)
            return;

        head.targetPosition = applyFrom.position;
        head.targetRotation = applyFrom.rotation.eulerAngles;
    }
}
