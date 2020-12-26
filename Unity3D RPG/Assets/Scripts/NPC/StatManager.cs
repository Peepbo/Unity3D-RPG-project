using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatInfo
{
    public bool isFirst;

    public int myId;
    public int priorId;

    //public string description;
}

public class StatManager : MonoBehaviour
{
    public List<StatInfo> characteristic = new List<StatInfo>();

    private void Awake()
    {
        //전투
        StatInfo _info = null;

        for(int i = 0; i < 35; i++)
        {
            _info.myId = i;

            if (i == 0 || i == 10 || i == 17 || i == 26 || i == 29 || i == 31)
                _info.isFirst = true;
            else 
                _info.isFirst = false;

            characteristic.Add(_info);
        }

        for(int i = 0; i < 10; i++)
        {
            if (characteristic[i].isFirst) continue;

            if (i == 1 || i == 4 || i == 7) characteristic[i].priorId = 0;

            else characteristic[i].priorId = i - 1;
        }

        for(int i = 10; i < 17; i++)
        {
            if (characteristic[i].isFirst) continue;

            if (i == 11 || i == 14) characteristic[i].priorId = 10;

            else characteristic[i].priorId = i - 1;
        }

        for(int i = 17; i < 26; i++)
        {
            if (characteristic[i].isFirst) continue;

            if (i == 18 || i == 21) characteristic[i].priorId = 17;

            else if (i < 24 || i == 25) characteristic[i].priorId = i - 1;

            else if (i == 24) characteristic[i].priorId = 22;
        }

        for(int i = 26; i < 35; i++)
        {
            if (characteristic[i].isFirst) continue;

            if (i <= 28 || i == 30 || i > 31) characteristic[i].priorId = i - 1;
        }
    }

    //private void Update()
    //{
    //    currencyText.text = "Currency : " +
    //        PlayerData.Instance.myCurrency.ToString();
    //}
}
