using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAni : MonoBehaviour
{
    public GameObject noTouch_1;
    public GameObject noTouch_2;
    public GameObject noTouch_3;
    public GameObject noTouch_4;

    private void OnEnable()
    {
        transform.parent.GetComponent<CheckingLoot>().ShowLoots();
    }

    public void QuitButton() { gameObject.SetActive(false); }

    public void CanTouch()
    {
        noTouch_1.transform.GetComponent<Button>().enabled = true;
        noTouch_2.transform.GetComponent<Button>().enabled = true;
        noTouch_3.transform.GetComponent<Button>().enabled = true;
        noTouch_4.transform.GetComponent<Button>().enabled = true;

        UiManager0.Instance.PanelOpen = false;
    }
}
