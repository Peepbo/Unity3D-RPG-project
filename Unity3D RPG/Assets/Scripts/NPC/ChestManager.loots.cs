using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

partial class ChestManager
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetLootsData()
    {
        int num = 0;

        for (int i = 1; i < 16; i++)
        {

            if (num == rootList.Count)
            {
                lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().color = Color.clear; // 표시 X
                popInfo.transform.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = null;
                continue;
            }

            else
            {
                lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().sprite = GetPath(4);

                int _itemNumber = rootList[i - 1].id;

                ItemInfo _item = CSVData.Instance.find(_itemNumber);

                if (_item.grade == "normal")
                    lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().color = color[0];
                else
                    lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().color = color[1];

                popInfo.transform.GetChild(i-1).GetChild(1).GetComponent<Text>().text =rootList[i-1].count.ToString();
                
            }
            num++;
        }
    }
}
