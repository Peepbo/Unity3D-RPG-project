using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<string> chestItem;
    // Start is called before the first frame update
    void Start()
    {
        chestItem = new List<string> ();

        PlayerData.Instance.item.Add(ItemCSV.Instance.find("OldNecklace"));

        //PlayerData.Instance.item.Add(ItemCSV.Instance.find("OldArmour"));

        //chestItem.Add(ItemCSV.Instance.find("OldArmour").itemName);
        //chestItem[1] = ItemCSV.Instance.find("OldArmour").itemName;
        //chestItem[2] = ItemCSV.Instance.find("OldArmour").itemName;
        //chestItem[2] = (ItemCSV.Instance.find("OldNecklace").itemName);

        //Debug.Log(chestItem[4]);
    }

    void chestUpdate()
    {
        //for (int i = 0; i < PlayerData.Instance.item.Count; i++)
        //{
        //    chestItem.Add(PlayerData.Instance.item[i].itemName);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
