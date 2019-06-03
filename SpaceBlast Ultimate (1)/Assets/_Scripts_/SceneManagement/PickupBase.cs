using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBase : MonoBehaviour  //Bogdan C. SU17A\\
{
    public GameObject effect;
    public bool entered;

    public void Start()
    {
        entered = false;
    }

    public void OnTriggerEnter(Collider other)
    {
            entered = true;
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
    }
}
