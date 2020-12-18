using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<ItemInfo> chestItem;
    // Start is called before the first frame update
    void Start()
    {
        PlayerData.Instance.LoadData();
        chestItem = new List<ItemInfo> ();

        //CSVData.Instance.CSVSave(200, "OldItem", "FastRun", "Resources/testPlayerSave.csv");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ItemInfo _item = CSVData.Instance.find("OldArmour");
            PlayerData.Instance.SaveChest(_item);

            Debug.Log(_item.itemName + " " + _item.count + "개 창고로 이동");
        }
    }
}
