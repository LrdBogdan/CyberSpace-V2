using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupBase
{
    // Update is called once per frame
    void Update()
    {
        if(entered == true)
        {
            Flasher();
        }
    }

    IEnumerator Flasher() //Flashes Objects Material with colour Red
    {
        MeshRenderer meshRen = GetComponentInChildren<MeshRenderer>();

        Color[] startColor = new Color[meshRen.materials.Length];

        for (int i = 0; i < startColor.Length; i++)
        {
            startColor[i] = meshRen.materials[i].color;
            meshRen.materials[i].SetColor("_BaseColor", Color.blue);
        }

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < startColor.Length; i++)
        {
            meshRen.materials[i].SetColor("_BaseColor", startColor[i]);
        }
    }
}
