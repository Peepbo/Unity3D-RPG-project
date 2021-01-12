using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //public ChestManager chestData;
    //public StatManager statData;
    // Start is called before the first frame update
   
    public void LoadData()
    {
        PlayerData.Instance.LoadData_v2();
    }

    public void ClosePanel()
    {
        UiManager0.Instance.PanelOpen = false;
    }
}
