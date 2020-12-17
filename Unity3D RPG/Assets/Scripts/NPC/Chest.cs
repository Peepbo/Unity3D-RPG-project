using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<ItemInfo> chestItem;
    // Start is called before the first frame update
    void Start()
    {
        chestItem = new List<ItemInfo> ();

        //CSVData.Instance.CSVSave(200, "OldItem", "FastRun", "Resources/testPlayerSave.csv");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlayerData.Instance.ChangeStat("atk", 0);
        }
    }
}
