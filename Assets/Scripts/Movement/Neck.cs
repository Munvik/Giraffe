using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neck : MonoBehaviour
{
    public Transform neckSpot;
    public List<Transform> neckVerts;
    public Transform head;

    private void Update()
    {
        int vertCount = neckVerts.Count;
        float step = 1f / vertCount;
        for(int i = 0; i < vertCount; i++)
        {
            neckVerts[i].transform.position = Vector3.Lerp(neckSpot.position, head.position, step * i);
        }
    }
}
