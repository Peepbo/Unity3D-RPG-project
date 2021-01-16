using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDeActive : MonoBehaviour
{
    public GetItemInfo info;
    public void DeActive()
    {
        //info.panelTime = 0;
        //info.panelList.RemoveAt(0);
        transform.parent.gameObject.SetActive(false);
    }
}
