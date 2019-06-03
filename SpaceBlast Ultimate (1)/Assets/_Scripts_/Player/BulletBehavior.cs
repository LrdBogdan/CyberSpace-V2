using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour //Bogdan C. SU17A - 2019©\\
{
    public ParticleSystem particles;

    //Script only manages existence of "bullet" a.k.a only funtcions: destroy, particle\\

    void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            GameObject particle = Instantiate(particles.gameObject, contact.point, Quaternion.LookRotation(contact.normal));
            Destroy(gameObject);
            Destroy(particle, 1);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        GameObject particle = Instantiate(particles.gameObject, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(particle, 1);
    }
}
