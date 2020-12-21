using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public StatManager statData;
    public ChestManager chestData;
    // Start is called before the first frame update
    void Start()
    {
        PlayerData.Instance.LoadData();
        statData.ListUpdate();
        chestData.GetData();

        Debug.Log("data roading end");
    }
}
