using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAni : MonoBehaviour
{
    public GameObject panel;
    public bool isOpen;

    public void OpenPanelAni()
    {
        transform.parent.GetComponent<CheckingLoot>().ShowLoots();
        panel.GetComponent<Animator>().SetBool("Open", true);
    }
    public void ClosePanelAni()
    {
        panel.GetComponent<Animator>().SetBool("Open", false);
    }
    public void ClickSound()
    {
        SoundManager.Instance.SFXPlay2D("UI_Click", 0.6f);
    }
    public void SellSound()
    {
        SoundManager.Instance.SFXPlay2D("UI_ItemSell", 0.6f);
    }
}
