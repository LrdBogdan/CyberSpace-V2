using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public partial class TurretDemo : MonoBehaviour
{
    public GameObject[] turrets;

    public virtual void Update()
    {
        if (turrets != null)
        {
            foreach (GameObject turret in (this.turrets as GameObject[]))
                turret.SendMessage("Target", this.transform.position);
        }
    }
}