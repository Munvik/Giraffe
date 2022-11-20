using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    public UnityAction OnCollisionDetected;

    private void OnTriggerEnter(Collider other)
    {
        OnCollisionDetected?.Invoke();
    }
}
