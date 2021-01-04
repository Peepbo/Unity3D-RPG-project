using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    protected UiManager() { }

    bool isOpen = false;

    public bool PanelOpen
    {
        get { return isOpen; }
        set { isOpen = value; }
    }
}
