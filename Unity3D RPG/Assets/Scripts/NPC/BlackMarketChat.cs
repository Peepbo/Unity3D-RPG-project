using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarketChat : MonoBehaviour
{
    public GameObject BlackMarketPanel;
    public GameObject chatPanel;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            BlackMarketPanel.SetActive(true);
            UiManager0.Instance.PanelOpen = true;
            chatPanel.SetActive(true);
        }
    }

    #region BUTTON FUNCTION
    public void ExitChat()
    {
        UiManager0.Instance.PanelOpen = false;
        chatPanel.SetActive(false);
    }
    #endregion
}
