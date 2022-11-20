using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformsFollowers : MonoBehaviour
{
    public Transform main;
    public Transform follower;

    private void Update()
    {
        follower.transform.position = main.transform.position;
        follower.transform.rotation = main.transform.rotation;  
    }
}
