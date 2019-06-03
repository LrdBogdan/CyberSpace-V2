using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour //Bogdan C. SU17A - 2019©\\
{
    #region Variables 
    public GameObject wave1, wave2, wave3, wave4, wave5;

    private float timer;
    public int internaltime;
    #endregion

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1f)
        {
            internaltime += 1;
            timer = 0;
        }

        if (internaltime > 3)
        {
            wave1.SetActive(true);
        }

        if (internaltime > 45)
        {
            wave2.SetActive(true);
        }

        if (internaltime > 90)
        {
            wave3.SetActive(true);
        }


        if (internaltime > 135)
        {
            wave4.SetActive(true);
        }

        if (internaltime > 170)
        {
            wave4.SetActive(true);
        }
    }
}
