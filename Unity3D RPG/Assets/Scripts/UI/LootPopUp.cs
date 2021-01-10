//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections;
//using UnityEngine.UI;
//using UnityEngine;
//using UnityEditor;

//partial class LootManager
//{
//    public GameObject popLoot;
//    ChestManager cm = new ChestManager();

//    Sprite GetPath(int id)
//    {
//        return Resources.Load<Sprite>(CSVData.Instance.find(id).iconPath);
//    }

//    public void ShowPocketData()
//    {
//        for(int i = 0; i < 10; i++)
//        {
//            if((i+1) > pocketItem.Count)
//            {
//                cm.lootsData[i].transform.GetChild(0).GetComponent<Image>().color = Color.clear;
//                popLoot.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = null;
//                continue;
//            }

//            else
//            {
//                int _itemNumber = PlayerData.Instance.haveLootItem[i].id;

//                cm.lootsData[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = GetPath(_itemNumber);
//                popLoot.transform.GetChild(i - 1).GetChild(1).GetComponent<Text>().text =
//                    PlayerData.Instance.haveLootItem[i].count.ToString();
//            }
//        }

//        //int num = 0;

//        //for (int i = 1; i < 11; i++)
//        //{
//        //    if (num == pd.myPocketItem.count)
//        //    {
//        //        cm.lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().color = Color.clear;
//        //        popLoot.transform.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = null;
//        //        continue;
//        //    }
//        //    else
//        //    {
//        //        int _itemNumber = cm.lootList[i - 1].id;

//        //        cm.lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().sprite = GetPath(_itemNumber);
//        //        popLoot.transform.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = cm.lootList[i - 1].count.ToString();
//        //    }
//        //    num++;
//        //}
//    }
//}