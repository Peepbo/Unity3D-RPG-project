using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public ChestManager chestData;
    public StatManager statData;
    // Start is called before the first frame update

    private void Start()
    {
        PlayerData.Instance.LoadData();
        chestData.MakeData();
        chestData.ItemUpdate();
        //chestData.OwnLoots();
        //chestData.GetLootsData();

        chestData.GetData();

        statData.ResetStat();
        statData.GetData();
    }
}
