using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class testInven : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
       // List<ItemInfo> a = new List<ItemInfo>();
       // a.Add(ItemCSV.Instance.find("OldSword"));
        Debug.Log(ItemCSV.Instance.find("NewSword").itemName);
        
    }

    // Update is called once per frame
 
}
