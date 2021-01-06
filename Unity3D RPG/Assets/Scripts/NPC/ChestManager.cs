using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class ChestManager : MonoBehaviour
{
    Color[] color = {
        new Color(1,1,1,1), // normal
        new Color(117f / 255, 1, 105f / 255, 1), // rare
    };

    //장착 장비
    public GameObject equips;
    public GameObject[] eqData;

    //소유 장비
    public GameObject slots;
    public GameObject[] gmData;

    //전리품
    public GameObject loots;
    public GameObject[] lootsData;

    //장비관련
    //public List<ItemInfo> equipList = new List<ItemInfo>();

    //전리품관련
    //public List<ItemInfo> lootList = new List<ItemInfo>();

    //정보 저장용
    public GameObject popInfo0; 
    public GameObject popInfo1; 
    int selectNumber;

    public void MakeData()
    {
        //left
        eqData = new GameObject[4];

        for(int i = 0; i < 4; i++)
        {
            eqData[i] = equips.transform.GetChild(i).gameObject;
        }

        //right
        gmData = new GameObject[16];
        for (int i = 0; i < 16; i++)
        {
            gmData[i] = slots.transform.GetChild(i).gameObject;
        }
    }

    //equipList와 rootList의 정보를 playerData의 myItem과 연동한다.
    public void ItemUpdate()
    {
        PlayerData.Instance.LoadData_v2();
        GetData();
    }

    public void OwnLoots()
    {
        lootsData = new GameObject[16];

        for (int i = 0; i < 16; i++)
        {
            lootsData[i] = loots.transform.GetChild(i).gameObject;
        }
    }


    Sprite GetPath(int id)
    {
        return Resources.Load<Sprite>(CSVData.Instance.find(id).iconPath);
    }

    public void Update()
    {
        RootUpdate();
    }
}
