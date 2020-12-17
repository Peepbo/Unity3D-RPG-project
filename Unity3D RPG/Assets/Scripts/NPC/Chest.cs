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

        PlayerData.Instance.myItem.Add(CSVData.Instance.find("OldNecklace"));

        //chestItem = PlayerData.Instance.item;
        //PlayerData.Instance.item.Add(ItemCSV.Instance.find("OldArmour"));

        //chestItem.Add(ItemCSV.Instance.find("OldArmour").itemName);
        //chestItem[1] = ItemCSV.Instance.find("OldArmour").itemName;
        //chestItem[2] = ItemCSV.Instance.find("OldArmour").itemName;
        //chestItem[2] = (ItemCSV.Instance.find("OldNecklace").itemName);

        //Debug.Log(chestItem[4]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
