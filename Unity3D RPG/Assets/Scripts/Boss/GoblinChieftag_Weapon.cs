using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinChieftag_Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GoblinChieftain chief;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //transform.GetComponentInParent<GoblinChieftain>().SetDamage();
            chief.SetDamage();
        }
        //Debug.Log("trigger");
    }
}


