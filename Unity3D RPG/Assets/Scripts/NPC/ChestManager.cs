using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class ChestManager : MonoBehaviour
{
    public GameObject slots;
    public GameObject[] gmData;

    //장비관련
    public List<ItemInfo> equipList = new List<ItemInfo>();

    //전리품관련
    public List<ItemInfo> rootList = new List<ItemInfo>();

    //정보 저장용
    public GameObject popInfo; 
    int selectNumber;

    public void MakeData()
    {
        gmData = new GameObject[16];
        for (int i = 0; i < 16; i++)
        {
            gmData[i] = slots.transform.Find("slot (" + i + ")").gameObject;
        }
    }

    //equipList와 rootList의 정보를 playerData의 myItem과 연동한다.
    public void ItemUpdate()
    {
        equipList.Clear();
        rootList.Clear();

        //equipList + rootList
        for (int i = 0; i < PlayerData.Instance.myItem.Count; i++)
        {
            ItemInfo _item = PlayerData.Instance.myItem[i];

            //4가 아니면?
            if (_item.kindID != 4) equipList.Add(_item);

            //4면?
            else rootList.Add(_item);
        }
    }
}
