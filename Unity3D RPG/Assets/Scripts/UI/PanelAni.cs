using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAni : MonoBehaviour
{
    private void OnEnable()
    {
        transform.parent.GetComponent<CheckingLoot>().ShowLoots();
    }

    public void QuitButton() { gameObject.SetActive(false); }
}
