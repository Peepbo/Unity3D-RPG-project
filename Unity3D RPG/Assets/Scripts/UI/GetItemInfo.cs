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

            int _number = 0;

            for(int i = 0; i < transform.childCount; i++) // childCount = 4
            {
                var child = transform.GetChild(i).gameObject;

                if(child.activeSelf)
                {
                    _number++;
                    continue;
                }

                if(_number == 4)
                {
                    //맨 앞에 존재하는 애를 끝내줘야한다.
                    //var _anim = panelList[0].obj.GetComponentInChildren<Animator>();
                    //_anim.SetTrigger("Close");
                    panelTime = 0;
                    panelList[0].obj.SetActive(false);
                    panelList.RemoveAt(0);

                    child.SetActive(true);

                    child.transform.position = pt[3].transform.position;

                    panelList.Add(new ItemPanel(child, ""));
                }

                else
                {
                    child.SetActive(true);
                    //child.transform.position = panelList.

                    int _index = panelList.Count;
                    _index = Mathf.Clamp(_index, 0, 3);

                    Debug.Log(_index);
                    child.transform.position = pt[_index].transform.position;

                    panelList.Add(new ItemPanel(child, ""));
                }

                break;
            }
        }

        if(panelList.Count > 0)
        {
            //move position
            for (int i = 0; i < panelList.Count; i++)
            {
                if (i == 4) break;

                panelList[i].obj.transform.position
                    = Vector3.Lerp(panelList[i].obj.transform.position,
                    pt[i].transform.position, Time.deltaTime*10f);
            }

            //del
            panelTime += Time.deltaTime;

            if (panelTime > 0.5f)
            {
                panelTime = 0;

                panelList.RemoveAt(0);
                panelList[0].obj.GetComponentInChildren<Animator>().SetTrigger("Close");
            }
        }

        
    }
}
