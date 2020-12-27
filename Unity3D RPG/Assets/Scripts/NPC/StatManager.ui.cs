using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

partial class StatManager
{
    [Header("Panel")]
    public Transform atkPanel;
    public Transform defPanel;
    public Transform sklPanel;
    public Transform utlPanel;

    [Header("Image")]
    public Image[] atkImg = new Image[10];
    public Image[] defImg = new Image[7];
    public Image[] sklImg = new Image[9];
    public Image[] utlImg = new Image[9];

    void GetImage()
    {
        for (int i = 1; i < atkPanel.childCount; i++)
        {
            atkImg[i - 1] = atkPanel.GetChild(i).GetComponent<Image>();
        }

        for (int i = 1; i < defPanel.childCount; i++)
        {
            defImg[i - 1] = defPanel.GetChild(i).GetComponent<Image>();
        }

        for (int i = 1; i < sklPanel.childCount; i++)
        {
            sklImg[i - 1] = sklPanel.GetChild(i).GetComponent<Image>();
        }

        for (int i = 1; i < utlPanel.childCount; i++)
        {
            utlImg[i - 1] = utlPanel.GetChild(i).GetComponent<Image>();
        }
    }

    public void ChangeColor()
    {
        ChangeColor_S2E(0, 10, ref atkImg);
        ChangeColor_S2E(10, 17, ref defImg);
        ChangeColor_S2E(17, 26, ref sklImg);
        ChangeColor_S2E(26, 35, ref utlImg);
    }

    void ChangeColor_S2E(int start, int end, ref Image[] imgArr)
    {
        for (int i = start; i < end; i++)
        {
            int _PriorId = characteristic[i].priorId;

            if (characteristic[i].isFirst == false &&
                characteristic[i].isLearn == false &&
                characteristic[_PriorId].isLearn == false) imgArr[i - start].color = Color.black;

            else if (characteristic[i].isLearn) imgArr[i - start].color = Color.blue;

            else imgArr[i - start].color = Color.white;
        }
    }
}