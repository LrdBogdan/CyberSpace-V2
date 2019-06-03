using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekSteer : MonoBehaviour
{
    #region Variables
    public Transform[] waypoints;
    public float waypointRadius = 1.5f;
    public float damping = 0.1f;
    public float speed = 2.0f;

    public bool loop = false;
    public bool faceHeading = true;
    private bool useRigidbody;

    private Vector3 currentHeading, targetHeading;
    private Transform xform;
    private int targetWaypoint;
    private Rigidbody rBody;
    #endregion

    protected void Start()
    {
        xform = transform;
        currentHeading = xform.forward;

        if (waypoints.Length <= 0)
        {
            Debug.Log("No waypoints on " + name);
            enabled = false;
        }

        targetWaypoint = 0;
    }

    //New Direction of Body
    protected void FixedUpdate()
    {
        targetHeading = waypoints[targetWaypoint].position - xform.position;

        currentHeading = Vector3.Lerp(currentHeading, targetHeading, damping * Time.deltaTime);
    }

    // Move on Z-axis
    protected void Update()
    {
        if (useRigidbody)
            rBody.velocity = currentHeading * speed;
        else
            xform.position += currentHeading * Time.deltaTime * speed;

        if (faceHeading)
            xform.LookAt(xform.position + currentHeading);

        if (Vector3.Distance(xform.position, waypoints[targetWaypoint].position) <= waypointRadius)
        {
            targetWaypoint++;
            if (targetWaypoint >= waypoints.Length)
            {
                targetWaypoint = 0;
                if (!loop)
                    enabled = false;
            }
        }
    }


    // KamiK's Draw a Line to Target
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (waypoints == null)
            return;
        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 pos = waypoints[i].position;
            if (i > 0)
            {
                Vector3 prev = waypoints[i - 1].position;
                Gizmos.DrawLine(prev, pos);
            }
        }
    }
}
