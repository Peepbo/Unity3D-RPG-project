using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAni : MonoBehaviour
{
    public GameObject panel;
    public bool isOpen;

    public void OpenPanelAni()
    {
        panel.GetComponent<Animator>().SetBool("Open", true);
    }
    public void ClosePanelAni()
    {
        panel.GetComponent<Animator>().SetBool("Open", false);
    }
}
