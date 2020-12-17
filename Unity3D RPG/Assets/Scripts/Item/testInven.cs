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
        Debug.Log(CSVData.Instance.find("NewSword").itemName);
        //CSVData.Instance.CSVSave(200, "OldItem", "FastRun", "Resources/testPlayerSave.csv");
    }

    // Update is called once per frame
 
}
