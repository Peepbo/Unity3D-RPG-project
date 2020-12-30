using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
partial class SmathManager
{
    //List<ItemInfo> accList = new List<ItemInfo>();
    Dictionary<int, ItemInfo> accList = new Dictionary<int, ItemInfo>();
    Dictionary<int, bool> accCheck = new Dictionary<int, bool>();
    TextMeshProUGUI[] accListText;
    const int maxAcc = 35;
    const int startNum = 44;
    public void OnAccButton()
    {
        for (int i = 0; i < maxAcc; i++)
        {
            itemList[i].SetActive(true);
            if (!itemList[i].GetComponent<Button>().interactable) ListDisable(i);
            AccListSetting(i);
        }
    }

    private void AccListSetting(int num)
    {
        accListText = itemList[num].GetComponentsInChildren<TextMeshProUGUI>();
      
        accListText[0].text = accList[num].skillIncrease + "티어 " + accList[num].itemName;
        accListText[1].text = accList[num].kind;
        accListText[2].text = accList[num].grade;
        if (accCheck[num]) ListDisable(num);
    }

    private void AccListInsert(int id)
    {
        ItemInfo _itemDB = CSVData.Instance.find(id);
        accList.Add(id-startNum, _itemDB);
        accCheck.Add(id - startNum, true);
    }

    private void AccListSerch()
    {
        for (int i = 0; i < maxAcc; i++)
        {
            if (accList.ContainsKey(i) == false)
            {
                accList.Add(i, CSVData.Instance.find(startNum+i));
                accCheck.Add(i, false);
            }
        }

    }
    
}
