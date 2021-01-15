using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetItemInfo : MonoBehaviour
{
    public class ItemPanel
    {
        public GameObject obj;
        public float time;
        public string text;

        public ItemPanel(GameObject panel, string str)
        {
            obj = panel;
            time = 3f;
            text = str;
        }
    }

    List<ItemPanel> panelList = new List<ItemPanel>();

    #region start
    //private int currency;
    //private List<ItemInfo> getItem = new List<ItemInfo>();
    //public Text myText;

    //public GameObject[] dropItem = new GameObject[3];

    //public void ShowCurrency(int value)
    //{
    //    currency = value;
    //    //Debug.Log(currency);
    //    //currencyText.text = "골드를 " + currency.ToString() + "획득했습니다.";
    //}

    //public void ShowItem(List<ItemInfo> gainItem)
    //{
    //    getItem = gainItem;

    //    for (int i = 0; i < getItem.Count; i++)
    //    {
    //        //dropItem[i].SetActive(true);
    //    }
    //}
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //childCount = 내 자식 오브젝트 개수
            //GetChild(num) = 내 자식의 num번째 오브젝트

            for(int i = 0; i < transform.childCount; i++) // childCount = 3
            {
                var child = transform.GetChild(i).gameObject;

                if (child.activeSelf == false)
                {
                    child.SetActive(true);

                    ItemPanel pn = new ItemPanel(child, "");

                    panelList.Add(pn);
                    break;
                }
            }
        }

        for(int i = 0; i < panelList.Count; i++)
        {
            panelList[i].time -= Time.deltaTime;

            if(panelList[i].time < 0)
            {
                panelList[i].obj.SetActive(false);
                panelList.RemoveAt(i);
            }
        }
        
    }
}
