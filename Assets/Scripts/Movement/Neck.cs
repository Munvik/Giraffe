using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neck : MonoBehaviour
{
    public Transform neckSpot;
    public List<Transform> neckVerts;
    public Transform head;

    [SerializeField] private List<NeckLink> neckLinks;

    private void Update()
    {
        int vertCount = neckVerts.Count;
        float step = 1f / vertCount;

        for (int i = 0; i < neckLinks.Count - 1; i++)
        {
            Quaternion rot = Quaternion.LookRotation(neckLinks[i + 1].link.position - neckLinks[i].link.position);
            rot = Quaternion.Euler(neckLinks[i].additionalRotation + rot.eulerAngles);
            neckLinks[0].link.transform.rotation = rot;

            neckLinks[i].link.transform.position = Vector3.Lerp(neckSpot.position, head.position, step * i);
        }

        //int vertCount = neckVerts.Count;
        //float step = 1f / vertCount;
        //for(int i = 0; i < vertCount; i++)
        //{
        //    neckVerts[i].transform.position = Vector3.Lerp(neckSpot.position, head.position, step * i);
        //}
    }
}

[Serializable]
public class NeckLink
{
    public Transform link;
    public Vector3 additionalRotation;
}
