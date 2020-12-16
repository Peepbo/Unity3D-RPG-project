using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class testInven : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {

        Debug.Log(ItemCSV.Instance.find("OldSword").itemName);
        
    }

    // Update is called once per frame
 
}
