using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAni : MonoBehaviour
{
    private void OnEnable()
    {
        transform.parent.GetComponent<CheckingLoot>().ShowLoots();
    }

    public void ClickSound()
    {
        SoundManager.Instance.SFXPlay2D("UI_Click", 0.6f);
    }
    public void SellSound()
    {
        SoundManager.Instance.SFXPlay2D("UI_ItemSell", 0.6f);
    }

    public void QuitButton() { gameObject.SetActive(false); }
}
