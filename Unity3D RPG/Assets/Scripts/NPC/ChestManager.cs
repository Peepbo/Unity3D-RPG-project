using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
    public GameObject slots;
    public GameObject[] gmData;

    private void Awake()
    {
        gmData = new GameObject[16];
        for (int i = 0; i < 16; i++)
        {
            //string _name = "slot(" + i + ")";
            //Debug.Log(name);
            gmData[i] = slots.transform.Find("slot (" + i + ")").gameObject;
        }
    }

    public void GetData()
    {
        int num = 0;
        foreach (GameObject data in gmData)
        {
            if (num == PlayerData.Instance.myItem.Count) break;

            data.GetComponentInChildren<Text>().text = PlayerData.Instance.myItem[num].itemName;
            num++;
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L)) PlayerData.Instance.LoadData();

        //if (Input.GetKeyDown(KeyCode.Z)) GetData();
    }
}
