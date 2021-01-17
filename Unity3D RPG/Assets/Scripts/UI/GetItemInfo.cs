using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetItemInfo : MonoBehaviour
{
    public class ItemPanel
    {
        public GameObject obj;
        public string text;

        public ItemPanel(GameObject panel, string str)
        {
            obj = panel;
            text = str;
        }
    }

    public List<ItemPanel> panelList = new List<ItemPanel>();
    public RectTransform[] pt = new RectTransform[4];

    public float panelTime = 0;
    bool firstIn = true;

    public int panelCount = 0;

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
        #region FUNCTION
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //childCount = 내 자식 오브젝트 개수
            //GetChild(num) = 내 자식의 num번째 오브젝트

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;

                if (child.activeSelf) { continue; }

                if (panelList.Count == 0) firstIn = true;

                if (panelList.Count > 3)
                {
                    firstIn = false;

                    panelTime = 0;

                    panelList[0].obj.GetComponentInChildren<Animator>().SetTrigger("Close");
                    panelList.RemoveAt(0);

                    child.SetActive(true);

                    child.transform.position = pt[3].transform.position;

                    panelList.Add(new ItemPanel(child, ""));
                }

                else
                {
                    child.SetActive(true);

                    int _index = panelList.Count;
                    _index = Mathf.Clamp(_index, 0, 3);

                    Debug.Log(_index);
                    child.transform.position = pt[_index].transform.position;

                    panelList.Add(new ItemPanel(child, ""));
                }

                break;
            }
        }
        #endregion

        #region UPDATE
        if (panelList.Count > 0)
        {
            //move position
            int _max = panelList.Count;
            for (int i = 0; i < _max; i++)
            {
                panelList[i].obj.transform.position
                    = Vector3.Lerp(panelList[i].obj.transform.position,
                    pt[i].transform.position, Time.deltaTime * 10f);
            }

            //del
            if (firstIn) { panelTime += Time.deltaTime * 0.5f; }

            else panelTime += Time.deltaTime;

            if (panelTime > 0.5f)
            {
                panelTime = 0;

                if (firstIn) firstIn = false;

                panelList[0].obj.GetComponentInChildren<Animator>().SetTrigger("Close");
                panelList.RemoveAt(0);
            }
        }
        #endregion
    }

    private void DisplayPanel(string panelText)
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

                panelList[0].obj.GetComponentInChildren<Animator>().SetTrigger("Close");
                panelList.RemoveAt(0);

                child.SetActive(true);

                child.transform.position = pt[3].transform.position;

                panelList.Add(new ItemPanel(child, panelText));
            }

            else
            {
                child.SetActive(true);

                int _index = panelList.Count;
                _index = Mathf.Clamp(_index, 0, 3);

                Debug.Log(_index);
                child.transform.position = pt[_index].transform.position;

                panelList.Add(new ItemPanel(child, panelText));
            }

            break;
        }
    }

    public void DisplayCurrency(int currency)
    {
        string text = currency.ToString() + "골드를 획득";
        DisplayPanel(text);
    }

    public void DisplayItem(List<ItemInfo> item)
    {
        string text = "";
        for(int i = 0; i < item.Count; i++)
        {
            text = item[i].itemName + "을 " + item[i].count + "개 획득";
            DisplayPanel(text);
        }
    }
}
