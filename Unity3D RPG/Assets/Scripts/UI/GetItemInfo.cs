using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetItemInfo : MonoBehaviour
{
    public List<GameObject> panelList = new List<GameObject>();
    public RectTransform[] pt = new RectTransform[4];

    public float panelTime = 0;
    bool firstIn = true;

    public Sprite[] sprite = new Sprite[2];

    private void Update()
    {
        if (panelList.Count > 0)
        {
            //move position
            int _max = panelList.Count;
            for (int i = 0; i < _max; i++)
            {
                panelList[i].transform.position
                    = Vector3.Lerp(panelList[i].transform.position,
                    pt[i].transform.position, Time.deltaTime * 10f);
            }

            if (firstIn) { panelTime += Time.deltaTime * 0.5f; }

            else panelTime += Time.deltaTime;

            if (panelTime > 0.5f)
            {
                panelTime = 0;

                if (firstIn) firstIn = false;

                panelList[0].GetComponentInChildren<Animator>().SetTrigger("Close");
                panelList.RemoveAt(0);
            }
        }
    }

    private void DisplayPanel(string panelText, int spriteNumber)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;

            if (child.activeSelf) { continue; }

            if (panelList.Count == 0) firstIn = true;

            if (panelList.Count > 3)
            {
                firstIn = false;

                panelTime = 0;

                panelList[0].GetComponentInChildren<Animator>().SetTrigger("Close");
                panelList.RemoveAt(0);

                child.SetActive(true);

                child.transform.position = pt[3].transform.position;

                child.transform.GetChild(0).GetComponentInChildren<Text>().text
                    = panelText;
                child.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite
                    = sprite[spriteNumber];

                panelList.Add(child);
            }

            else
            {
                child.SetActive(true);

                int _index = panelList.Count;
                _index = Mathf.Clamp(_index, 0, 3);

                child.transform.position = pt[_index].transform.position;

                child.transform.GetChild(0).GetComponentInChildren<Text>().text
                    = panelText;
                child.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite
                    = sprite[spriteNumber];

                panelList.Add(child);
            }

            break;
        }
    }

    public void DisplayCurrency(int currency)
    {
        string text = currency.ToString() + "골드를 획득";
        DisplayPanel(text, 0);
    }

    public void DisplayItem(List<ItemInfo> item)
    {
        string text = "";
        for(int i = 0; i < item.Count; i++)
        {
            text = item[i].itemName + "을 " + item[i].count + "개 획득";
            DisplayPanel(text, 1);
        }
    }
}
