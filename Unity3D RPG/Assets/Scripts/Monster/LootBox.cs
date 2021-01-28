using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootBox : MonoBehaviour
{
    int dropRate;
    int min, max;
    int currency;
    List<ItemInfo> item = new List<ItemInfo>();
    GetItemInfo showItemPanel;

    #region COLLIDER & LIGHT
    BoxCollider col;
    public Light light;
    float colTime;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        light.intensity = 0;
        showItemPanel = GameObject.Find("GetItemInfo").GetComponent<GetItemInfo>();

    }

    private void Update()
    {
        if(!col.enabled)
        {
            colTime += Time.deltaTime;
            light.intensity = Mathf.Lerp(light.intensity, 2, Time.deltaTime);

            if (colTime > 1) col.enabled = true;
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Drop();
            SoundManager.Instance.SFXPlay2D("UI_ItemGet");
            gameObject.SetActive(false);
        }
    }

    public void setItemInfo(List<ItemInfo> dropItem, int rate,int minGold,int maxGold)
    {
        item = dropItem;
        dropRate = rate;
        min = minGold;
        max = maxGold;
    }


    public void Drop()
    {

        currency = Random.Range(min, max + 1);
        LootManager.Instance.GetPocketMoney(currency);

        //UI띄우기 용
        showItemPanel.DisplayCurrency(currency);

        List<ItemInfo> _itemList = new List<ItemInfo>();
        
        //아이템 드롭 개수 결정
        for (int i = 0; i < item.Count; i++)
        {
            int rate = Random.Range(0, 10);

            if (dropRate > rate) continue;

            if (dropRate <= rate)
            {
                _itemList.Add(item[i]);
            }
        }

        if (_itemList.Count != 0)
        {
            LootManager.Instance.GetPocketData(_itemList);

            //UI띄우기 용
            showItemPanel.DisplayItem(_itemList);
        }
    }


    //Animation event
    public void DropSound()
    {
        SoundManager.Instance.SFXPlay("ItemBox_Appear", transform.position);
    }
}
