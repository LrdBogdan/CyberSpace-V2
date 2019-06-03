using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerShoot : MonoBehaviour //Bogdan C. SU17A - 2019©\\
{
    #region Variables
    public float moveForce = 0f;
    private Rigidbody rBody;
    public GameObject bullet, superBullet;

    public ParticleSystem muzzleFlashLeft;
    public ParticleSystem muzzleFlashRight;

    public Slider bombFill;
    public float currentBomb;
    public float maxBomb;
    public float plantedBombs = 0;
    public float bombBarYOffset = 1;
    public bool canBomb, reloading;

    public Transform[] firePoints;
    public Transform bombPoints;

    public float fireRate = 0f;
    public float fireForce = 0f;
    private float fireRateTimeStamp = 0f;
    #endregion

    void Start()
    {
        canBomb = true;
        currentBomb = 9;
        maxBomb = 9;
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal") * moveForce;
        float v = Input.GetAxis("Vertical") * moveForce;

        rBody.velocity = new Vector3(h, v, 0);

        if (plantedBombs > 3 && canBomb == true)
        {
            canBomb = false;
            StartCoroutine(BombTimer());
            reloading = true;
        }

        if (currentBomb > 1 && reloading == false)
        {
            canBomb = true;
        }

        if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > fireRateTimeStamp)
            {
                muzzleFlashLeft.Play();
                muzzleFlashRight.Play();
                FindObjectOfType<AudioManager>().Play("PlayerFire");

                foreach (Transform f in firePoints) // For All the prayers
                {
                    GameObject go1 = (GameObject)Instantiate(bullet, f.position, f.rotation);
                    go1.GetComponent<Rigidbody>().AddForce(f.forward * fireForce);
                    fireRateTimeStamp = Time.time + fireRate;
                }
            }
        }

        if (Input.GetButtonDown("B") || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Time.time > fireRateTimeStamp && canBomb == true)
            {
                plantedBombs += 1;
                ChangeSlider(-3);
                muzzleFlashLeft.Play();
                muzzleFlashRight.Play();
                FindObjectOfType<AudioManager>().Play("PlayerBomb");

                GameObject go1 = (GameObject)Instantiate(superBullet, bombPoints.position, bombPoints.rotation);
                go1.GetComponent<Rigidbody>().AddForce(bombPoints.forward * fireForce);
                fireRateTimeStamp = Time.time + fireRate;
            }
        }
    }

    void Shoot() { 
}

    public void Reloaded()
    {
        reloading = false;
    }

    public void ChangeSlider(int amount)
    {
        currentBomb += amount;
        currentBomb = Mathf.Clamp(currentBomb, 0, maxBomb);
        bombFill.value = currentBomb;
    }

    IEnumerator BombTimer()
    {
        for (int i = 0; i < 10; i++)
        {
            ChangeSlider(1);
            yield return new WaitForSecondsRealtime(1f);
        }

        plantedBombs = 0;
        FindObjectOfType<AudioManager>().Play("BombRecharge");
        Reloaded();
    }
}
