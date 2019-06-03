using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Bogdan C. SU17A - 2019©\\

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    #region Variables
    public Transform healthBar;
    public Slider healthFill;
    public GameObject explosion;
    public GameObject childExploding;

    public static bool playerDied = false;

    public float currentHealth;
    public float maxHealth;
    public float healthBarYOffset = 2;

    public int InternalKillPoints { get; set; }
    #endregion

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Removes damage from current hp
        CheckIfDead();
        ChangeHealth(-1);
    }

    public void CheckIfDead()
    {
        if (currentHealth <= 1)
        {
            playerDied = true;

            if (gameObject.tag == "Player" && playerDied == true)
            {
                OnDeath();
            }
        }

        else
        {
            playerDied = false;
        }
    }

    public void OnDeath()
    {
        Time.timeScale = 2f;
        Instantiate(explosion, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Destroy(childExploding); // Destroy self
        StartCoroutine(PlayerDied(2));
    }

    IEnumerator PlayerDied(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log(gameObject.name + " has died");
        GameController.GameOver();
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Play("GameOver");
    }

    public void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Projectile": // If hit by PlayerBullet
                if (gameObject.tag == "Projectile") { break; }
                else
                {
                    StartCoroutine(Flasher());
                    FindObjectOfType<AudioManager>().Play("PlayerHit");
                    TakeDamage(1);
                }

                break;

            case "Enemy": // If hit by Enemy
                if (gameObject.tag == "Enemy") { break; }
                else
                {
                    StartCoroutine(Flasher());
                    FindObjectOfType<AudioManager>().Play("PlayerHit");
                    TakeDamage(1);
                }

                break;

            case "Pickup(H)": // If hit by Pickup
                if (gameObject.tag == "Pickup(H)") { break; }
                else
                {
                    FindObjectOfType<AudioManager>().Play("Health");
                    currentHealth = 6;
                    healthFill.value = maxHealth;
                }

                break;

            case "Bomb":
                if (gameObject.tag == "Bomb") { break; }
                else
                {
                    StartCoroutine(Flasher());
                    FindObjectOfType<AudioManager>().Play("PlayerHit");
                    TakeDamage(10);
                }

                break;

            default:

                Debug.Log("Unknown tag");
                break;

        }
    }

    IEnumerator Flasher() // Flashes red when trigger enetered
    {
        MeshRenderer meshRen = GetComponentInChildren<MeshRenderer>();

        Color[] startColor = new Color[meshRen.materials.Length];

        for (int i = 0; i < startColor.Length; i++)
        {
            startColor[i] = meshRen.materials[i].color;
            meshRen.materials[i].SetColor("_BaseColor", Color.white);
        }

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < startColor.Length; i++)
        {
            meshRen.materials[i].SetColor("_BaseColor", startColor[i]);
        }
    }

    public void ChangeHealth(int amount) // Changes health slider
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthFill.value = currentHealth;
    }
}
