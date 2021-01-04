using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

partial class LootManager
{
    public GameObject popLoot;
    ChestManager cm;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    Sprite GetPath(int id)
    {
        return Resources.Load<Sprite>(CSVData.Instance.find(id).iconPath);
    }

    public void ShowPocketData()
    {
        int num = 0;

        for (int i = 1; i < 11; i++)
        {
            if (num == pd.myPocketItem.count)
            {
                cm.lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().color = Color.clear;
                popLoot.transform.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = null;
                continue;
            }
            else
            {
                int _itemNumber = cm.lootList[i - 1].id;

                cm.lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().sprite = GetPath(_itemNumber);
                popLoot.transform.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = cm.lootList[i - 1].count.ToString();
            }
            num++;
        }
    }
}