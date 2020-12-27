using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePopUp : MonoBehaviour
{
    public string setName;
    public Text popName;
    public Button popButton;
    string popData;

    public void ChangeData(string _name, string _popData)
    {
        popName.text = _name;
        popData = _popData;
    }

    public void Update()
    {
        popName.text = setName;
    }
}
