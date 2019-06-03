using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour //Bogdan C. SU17A - 2019©\\
{
    #region Variables
    public GameObject Shield;
    public GameObject Player;
    public bool shieldActive = false;
    public float shieldTimer;
    public bool pickedUP = false;

    public Slider shieldFill;
    public float currentShield;
    public float maxShield;
    public float ShieldBarYOffset = 1;
    #endregion

    void Start()
    {
        currentShield = 0;
        maxShield = 0;
        shieldTimer = 0;
    }

    void Update()
    {
        if (pickedUP == true)
        {
            maxShield = 8;
            ActiveSlider();
            shieldTimer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Y") && shieldTimer > 1)
        {
            if (Player != null)
            {
                FindObjectOfType<AudioManager>().Play("ShieldRecharge");
                Shield.gameObject.SetActive(true);
            }

            shieldTimer = 3;
            pickedUP = false;
            shieldActive = true;
            GetComponentInChildren<Collider>().enabled = false;
            PlayerShielded();
        }

        if (shieldTimer < 1)
        {
            if (Player != null)
            {
                GetComponentInChildren<Collider>().enabled = true;
            }

            Shield.gameObject.SetActive(false);
            shieldActive = false;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Pickup(S)" && pickedUP == false)
        {
            FindObjectOfType<AudioManager>().Play("Pickup");
            pickedUP = true;
        }
    }

    public void ActiveSlider()
    {
        currentShield = 8;
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);
        shieldFill.value = currentShield;
    }

    public void PlayerShielded()
    {
        StartCoroutine(SheildLoad());
    }

    public void ChangeSlider()
    {
        currentShield -= 1;
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);
        shieldFill.value = currentShield;
    }

    IEnumerator SheildLoad()
    {
        for (int i = 0; i < 8; i++)
        {
            ChangeSlider();
            yield return new WaitForSecondsRealtime(1f);
        }

        shieldTimer = 0;
        shieldActive = false;
        FindObjectOfType<AudioManager>().Play("ShieldDown");
    }
}
