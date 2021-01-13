using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISound : MonoBehaviour
{
    public GameObject noTouch_1;
    public GameObject noTouch_2;
    public GameObject noTouch_3;
    public GameObject noTouch_4;

    public void ClickSound()
    {
        SoundManager.Instance.SFXPlay2D("UI_Click", 0.6f);
    }
    public void SellSound()
    {
        SoundManager.Instance.SFXPlay2D("UI_ItemSell", 0.6f);
    }

    public void CanNotTouch()
    {
        noTouch_1.transform.GetComponent<Button>().enabled = false;
        noTouch_2.transform.GetComponent<Button>().enabled = false;
        noTouch_3.transform.GetComponent<Button>().enabled = false;
        noTouch_4.transform.GetComponent<Button>().enabled = false;
    }
    public void CanTouch()
    {
        noTouch_1.transform.GetComponent<Button>().enabled = true;
        noTouch_2.transform.GetComponent<Button>().enabled = true;
        noTouch_3.transform.GetComponent<Button>().enabled = true;
        noTouch_4.transform.GetComponent<Button>().enabled = true;
    }
    public void ClearItemData()
    {
        LootManager.Instance.ClearPocketData();
        LootManager.Instance.isDelivery = true;
    }
}
