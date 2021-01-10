using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PocketData
{
    public ItemInfo pocketItem = new ItemInfo();  // 던전에서 획득 한 전리품 (아이템)
    public int pocketMoney = 0;                   // 던전에서 획득 한 전리품 (돈)
    public ItemInfo myPocketItem = null;          // public ItemInfo myPocketItem = null;
}

public class CheckingLoot : MonoBehaviour
{
    public GameObject pocketPanel;
    PocketData pd = new PocketData();
    public GameObject checkMoney;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    Sprite GetPath(int id)
    {
        return Resources.Load<Sprite>(CSVData.Instance.findAchieve(id).icon);
    }

    public void ShowLoots()
    {
        checkMoney.transform.GetChild(1).GetComponent<Text>().text = LootManager.Instance.pocketMoney.ToString();

        for (int i = 0; i < 16; i++)
        {
            pocketPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = GetPath(int.Parse(pd.pocketItem.iconPath));
            pocketPanel.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = pd.pocketItem.count.ToString();
        }
    }
}