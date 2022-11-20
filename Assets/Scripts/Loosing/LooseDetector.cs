using Movement;
using Networking;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LooseDetector : MonoBehaviour
{
    public NetworkSender sender;
    public Transform headBody;
    public Transform body;
    public float distanceToLoose;

    public Body giraffeBody;
    public Transform headTove;

    private Vector3 startPositionVr;
    private Quaternion startRotationVr;

    [Header("LOose anim")]
    public Image hidePanel;
    public Vector2 anchorFrom;
    public Vector2 anchorTo;
    private bool animateScreen;
    private bool reseted;
    public float animDuration;
    private float animStart;

    public List<CollisionDetector> collisionDetectors = new List<CollisionDetector>();

    private void Awake()
    {
        startPositionVr = headTove.position;
        startRotationVr = headTove.rotation;

        foreach (var detector in collisionDetectors)
            detector.OnCollisionDetected += LaunchLoose;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LaunchLoose();

        if (animateScreen)
        {
            float progress = (Time.time - animStart) / animDuration;
            hidePanel.rectTransform.anchoredPosition = Vector2.Lerp(anchorFrom, anchorTo, progress);
            if (progress >= .5f && !reseted)
            {
                reseted = true;
                headTove.position = startPositionVr;
                headTove.rotation = startRotationVr;

                giraffeBody.targetMovement = 0f;
                giraffeBody.targetCrouch = 1f;
            }

            if (progress >= 1f)
            {
                reseted = false;
                animateScreen = false;
            }
        }


        float distance = Mathf.Abs(headBody.position.x) - Mathf.Abs(body.position.x);
        if (distance > distanceToLoose)
        {
            LaunchLoose();
            return;
        }       
    }

    public bool sendLoose;
    [ContextMenu("Loose")]
    public void LaunchLoose()
    {
        //if (animateScreen)
        //    return;

        //animStart = Time.time;
        //animateScreen = true;
        //reseted = false;
        
        //if(sendLoose)
        //    sender?.Send("Loose", "c_down");
    }

    [ContextMenu("Collect alll detectors")]
    public void CollectAllDetecotrs()
    {
        collisionDetectors = FindObjectsOfType<CollisionDetector>().ToList();
    }
}
