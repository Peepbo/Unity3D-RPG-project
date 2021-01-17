using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonResultPanel : MonoBehaviour
{
    public GameObject lootPanel;
    private void Start()
    {
        GameObject _title = transform.Find("Title").gameObject;
        GameObject _countdown = transform.Find("Countdown").gameObject;
        GameObject _touchPanel = transform.Find("TouchPanel").gameObject;
        ResultController.Instance.Init(_title,_countdown,_touchPanel,lootPanel);
    }
    public void OnClick()
    {
        ResultController.Instance.OnPanelClick();
    }

   
}
