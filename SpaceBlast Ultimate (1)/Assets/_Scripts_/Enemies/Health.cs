

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Health : MonoBehaviour, ITakeDamage
{
    #region Variables
    public float hp = 10f;
    public GameObject explosion;
    public GameObject score;
    public int killPoints = 1;

    public int InternalKillPoints { get; set; }
    #endregion

    void Start()
    {
        InternalKillPoints = killPoints;
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(Flasher());
        hp -= damage; // Removes damage from current hp
        CheckIfDead();
    }

    public void CheckIfDead()
    {
        if (hp <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        ScoreSystem.Instance.Internalscore += killPoints;
        Destroy(gameObject); // Destroy self
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(score, transform.position, transform.rotation);
        Debug.Log(gameObject.name + " has died");
    }

    public void OnTriggerEnter(Collider col)
    {

        switch (col.gameObject.tag)
        {
            case "Projectile": 
                if (gameObject.tag == "Projectile") { break; }
                else
                {
                    TakeDamage(1);
                }

                break;

            case "Bomb": 
                if (gameObject.tag == "Bomb") { break; }
                else
                {
                    TakeDamage(20);
                }

                break;

            case "LastLine":
                if (gameObject.tag == "LastLine") { break; }
                else
                {
                    ScoreSystem.Instance.Internalscore -= killPoints;
                    Destroy(gameObject); // Destroy self
                }

                break;

            case "Player":
                if (gameObject.tag == "Player") { break; }
                else
                {
                    TakeDamage(20);
                    ScoreSystem.Instance.Internalscore -= killPoints;
                }

                break;


            default:
                Debug.Log("Unknown tag");
                break;
        }
    }

    IEnumerator Flasher() //Flashes Objects Material with colour Red
    {
        MeshRenderer meshRen = GetComponentInChildren<MeshRenderer>();

        Color[] startColor = new Color[meshRen.materials.Length];

        for (int i = 0; i < startColor.Length; i++)
        {
            startColor[i] = meshRen.materials[i].color;
            meshRen.materials[i].SetColor("_BaseColor", Color.red);
        }

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < startColor.Length; i++)
        {
            meshRen.materials[i].SetColor("_BaseColor", startColor[i]);
        }
    }
}
