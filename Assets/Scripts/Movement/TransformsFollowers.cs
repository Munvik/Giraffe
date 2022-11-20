using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformsFollowers : MonoBehaviour
{
    public Transform main;
    public Transform follower;
    public Vector3 offset;

    private void Update()
    {
        follower.transform.position = main.transform.position + offset;
        follower.transform.rotation = main.transform.rotation;  
    }
}
