using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum MENU
{
    ATTACK,
    DEFENCE,
    SKILL,
    UTIL
}

public class StatInfo
{
    public bool isLearn;    //배웠는지?
    public bool isFirst;    //처음부터 찍을 수 있는 스킬

    public int myId;        //특성 번호
    public int priorId;     //선행 특성

    //public string description;
}

public partial class StatManager : MonoBehaviour
{
    public List<StatInfo> characteristic = new List<StatInfo>();
    public Image[] menuImg;
    MENU eMenu;

    int saveId;

    public void ResetStat()
    {
        if(characteristic.Count > 0) characteristic.Clear();

        for (int i = 0; i < 35; i++)
        {
            StatInfo _info = new StatInfo();

            _info.isLearn = false;
            _info.myId = i;

            if (i == 0 || i == 10 || i == 17 || i == 26 || i == 29 || i == 31)
            {
                _info.isFirst = true;
            }
            else
                _info.isFirst = false;

            characteristic.Add(_info);
        }

        for (int i = 0; i < 10; i++)
        {
            if (characteristic[i].isFirst) continue;

            if (i == 1 || i == 4 || i == 7) characteristic[i].priorId = 0;

            else characteristic[i].priorId = i - 1;
        }

        for (int i = 10; i < 17; i++)
        {
            if (characteristic[i].isFirst) continue;

            if (i == 11 || i == 14) characteristic[i].priorId = 10;

            else characteristic[i].priorId = i - 1;
        }

        for (int i = 17; i < 26; i++)
        {
            if (characteristic[i].isFirst) continue;

            if (i == 18 || i == 21) characteristic[i].priorId = 17;

            else if (i < 24 || i == 25) characteristic[i].priorId = i - 1;

            else if (i == 24) characteristic[i].priorId = 22;
        }

        for (int i = 26; i < 35; i++)
        {
            if (characteristic[i].isFirst) continue;

            if (i <= 28 || i == 30 || i > 31) characteristic[i].priorId = i - 1;
        }
    }

    public void GetData()
    {
        for(int i = 0; i < 35; i++)
        {
            if (PlayerData.Instance.myAbility[i] == 1) characteristic[i].isLearn = true;
            else characteristic[i].isLearn = false;
        }

        for (saveId = 0; saveId < 35; saveId++)
        {
            SetCharacteristic();
        }

        saveId = 0;
    }

    private void Awake()
    {
        eMenu = MENU.ATTACK;

        ResetStat();
        GetImage();
        ChangeColor();
    }

    public void ClickMenu(int menuNumber)
    {
        if (eMenu == (MENU)menuNumber) return;

        for(int i = 0; i < 4; i++)
        {
            if (i == menuNumber)
            {
                menuImg[i].color = Color.white;
                eMenu = (MENU)i;
            }

            else menuImg[i].color = Color.gray;
        }

        ChangePanel();
    }

    void ChangePanel()
    {
        switch (eMenu)
        {
            case MENU.ATTACK:
                atkPanel.gameObject.SetActive(true);
                defPanel.gameObject.SetActive(false);
                sklPanel.gameObject.SetActive(false);
                utlPanel.gameObject.SetActive(false);
                break;
            case MENU.DEFENCE:
                atkPanel.gameObject.SetActive(false);
                defPanel.gameObject.SetActive(true);
                sklPanel.gameObject.SetActive(false);
                utlPanel.gameObject.SetActive(false);
                break;
            case MENU.SKILL:
                atkPanel.gameObject.SetActive(false);
                defPanel.gameObject.SetActive(false);
                sklPanel.gameObject.SetActive(true);
                utlPanel.gameObject.SetActive(false);
                break;
            case MENU.UTIL:
                atkPanel.gameObject.SetActive(false);
                defPanel.gameObject.SetActive(false);
                sklPanel.gameObject.SetActive(false);
                utlPanel.gameObject.SetActive(true);
                break;
        }
    }

    public void SaveId(int id)
    {
        saveId = id;
    }

    public void SetCharacteristic()
    {
        //선행 스킬이 없을 때
        if(characteristic[saveId].isFirst)
        {
            characteristic[saveId].isLearn = true;

            PlayerData.Instance.SaveAbility(characteristic);
        }

        //선행 스킬이 있을 때
        else
        {
            int _prior = characteristic[saveId].priorId;

            //선행 스킬이 찍혀있지 않을 때
            if (characteristic[_prior].isLearn == false)
            {
                //Debug.Log("선행스킬 " + _prior + "가 찍혀있지 않습니다");
                return;
            }

            characteristic[saveId].isLearn = true;

            PlayerData.Instance.SaveAbility(characteristic);
        }

        //Debug.Log(id + "를 찍었습니다");

        ChangeColor();
    }
}