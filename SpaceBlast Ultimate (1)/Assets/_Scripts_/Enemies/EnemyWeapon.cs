using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyWeapon : MonoBehaviour //Bogdan C. SU17A - 2019©\\
{
    public Transform[] spawnPoints;
    public GameObject bullet;
    public float fireForce = 0f;
    public float fireRate = 2f;

    public void Start()
    {
        StartCoroutine(waitShoot());
    }

    private IEnumerator waitShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.75f, fireRate)); // Delay between shots
            Shoot();
        }
    }

    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("EnemyFire");

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject go1 = (GameObject)Instantiate(bullet, this.spawnPoints[spawnPointIndex].position, this.spawnPoints[spawnPointIndex].rotation);
        go1.GetComponent<Rigidbody>().AddForce(this.spawnPoints[spawnPointIndex].forward * fireForce);
    }
}
