using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public ChestManager chestData;
    // Start is called before the first frame update

    private void Start()
    {
        PlayerData.Instance.LoadData();
        chestData.MakeData();
        chestData.GetData();

        chestData.ItemUpdate();
    }
}
