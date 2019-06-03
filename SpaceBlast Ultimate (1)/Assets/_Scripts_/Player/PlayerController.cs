using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour //Bogdan C. SU17A - 2019©\\
{
    #region Variables
    public float hoverHeight = 3f;
    public float hoverHeightStrictness = 10f;
    public float playerMass = 5f;
    public float dragThreshold = 25f;
    public float superDrag = 2f;
    public float fastDrag = 0.5f;
    public float slowDrag = 0.01f;

    public float sqrdAngularSpeedThresholdForDrag = 5F;
    public float superADrag = 32f;
    public float fastADrag = 16f;
    public float slowADrag = 0.1f;
    public float bank;

    public bool playerControl = true;
    #endregion

    void SetPlayerControl(bool control)
    {
        playerControl = control;
    }

    void Start()
    {
        GetComponent<Rigidbody>().mass = playerMass;
    }

    void FixedUpdate() 
    {
        if (Mathf.Abs(thrust) > 0.02F)
        {
            if (GetComponent<Rigidbody>().velocity.sqrMagnitude > dragThreshold)
                GetComponent<Rigidbody>().drag = fastDrag;
            else
                GetComponent<Rigidbody>().drag = slowDrag;
        }
        else
            GetComponent<Rigidbody>().drag = superDrag;

        if (Mathf.Abs(turn) > 0.02F)
        {
            if (GetComponent<Rigidbody>().angularVelocity.sqrMagnitude > sqrdAngularSpeedThresholdForDrag)
            { GetComponent<Rigidbody>().angularDrag = fastADrag; }

            else
                GetComponent<Rigidbody>().angularDrag = slowADrag;
        }

        else
            GetComponent<Rigidbody>().angularDrag = superADrag;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, hoverHeight, transform.position.z), hoverHeightStrictness);

        Vector3 rotation = transform.rotation.eulerAngles;

        rotation *= Mathf.Deg2Rad;
        rotation.x = 0f;
        rotation.z = 0f;
        transform.rotation = Quaternion.EulerAngles(rotation);
        rotation *= Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(rotation);
    }

    float thrust = 0f;
    float turn = 0f;

    void Thrust(float t)
    {
        thrust = Mathf.Clamp(t, -1F, 1F);
    }


    bool thrustGlowOn = false;

    void Update()
    {
        float theThrust = thrust;

        if (playerControl)
        {
            thrust = Input.GetAxis("Vertical");
        }


        GetComponent<Rigidbody>().AddRelativeTorque(Vector3.up * turn * Time.deltaTime);
    }
}
