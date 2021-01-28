using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

partial class SmathManager
{
    //List<ItemInfo> accList = new List<ItemInfo>();
    Dictionary<int, ItemInfo> accList = new Dictionary<int, ItemInfo>();
    Dictionary<int, bool> accCheck = new Dictionary<int, bool>();
    Text[] accListText;
    const int maxAcc = 35;
    const int startNum = 44;
    bool isAcc = false;
    public void OnAccButton() //악세사리종류 버튼
    {
        ClickSound();
        isWeapon = false;
        isArmour = false;
        isAcc = true;

        for (int i = 0; i < maxAcc; i++)
        {
            itemList[i].GetComponent<Button>().onClick.RemoveAllListeners();
            itemList[i].SetActive(true);
            if (!itemList[i].GetComponent<Button>().interactable) ListDisable(i);
            AccListSetting(i);

            int _i = i;
            itemList[i].GetComponent<Button>().onClick.AddListener(delegate { OnAccClick(_i); });
        }
    }

    private void AccListSetting(int num) //아이템리스트에 악세사리 정보 띄우기.
    {
        accListText = itemList[num].GetComponentsInChildren<Text>();
      
        accListText[0].text = accList[num].skillIncrease + "티어 " + accList[num].itemName;
        accListText[1].text = accList[num].kind;
        accListText[2].text = accList[num].grade;
        if (accCheck[num]) ListDisable(num);
    }

    private void AccListInsert(int id) //플레이어 아이템을 악세사리리스트에 담는다.
    {
        ItemInfo _itemDB = CSVData.Instance.find(id);
        accList.Add(id-startNum, _itemDB);
        accCheck.Add(id - startNum, true);
    }

    private void AccListSerch() //악세사리 리스트중 없는 악세사리는 채운다.
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

    private void OnAccClick(int num) // 아이템리스트 번호와 맞는 악세사리리스트 정보를 현재 보여줘야할 아이템정보로 교체.
    {
        if (!isAcc) return;
        ClickSound();
        curruntInfo = accList[num];

        MaterialTextSetting();
    }

}
