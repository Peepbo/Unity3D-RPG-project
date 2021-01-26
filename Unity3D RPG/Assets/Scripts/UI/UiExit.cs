using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiExit : MonoBehaviour
{
    public GameObject panel;
    public void QuitButton()
    {
        gameObject.SetActive(false);
        panel.SetActive(false);
    }
}
