using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    //public ChestManager chestData;
    //public StatManager statData;
    // Start is called before the first frame update

    public GameObject noTouch_1;
    public GameObject noTouch_2;
   
    public void LoadData()
    {
        PlayerData.Instance.LoadData_v2();
    }

    public void ClosePanel()
    {
        UiManager0.Instance.PanelOpen = false;
    }

    public void CanNotTouch()
    {
        noTouch_1.transform.GetComponent<Button>().enabled = false;
        noTouch_2.transform.GetComponent<Button>().enabled = false;
    }

    public void CanTouch()
    {
        noTouch_1.transform.GetComponent<Button>().enabled = true;
        noTouch_2.transform.GetComponent<Button>().enabled = true;
    }
}
