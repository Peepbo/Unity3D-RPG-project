using UnityEngine;
using UnityEngine.UI;

public class CheckingLoot : MonoBehaviour
{
    public GameObject pocketPanel;
    public GameObject checkMoney;

    // Use this for initialization
    void Start()
    {
        ShowLoots();
    }

    // Update is called once per frame
    void Update()
    {

    }

    Sprite GetPath(int id)
    {
        return Resources.Load<Sprite>(CSVData.Instance.find(id).iconPath);
    }

    public void ShowLoots()
    {
        checkMoney.transform.GetChild(1).GetComponent<Text>().text = LootManager.Instance.pocketMoney.ToString();

        for (int i = 0; i < 10; i++)
        {
            if ((i + 1) > LootManager.Instance.pocketItem.Count)
            {
                pocketPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.clear;
                pocketPanel.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = "";

                continue;
            }

            pocketPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;

            pocketPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite =
                GetPath(int.Parse(LootManager.Instance.pocketItem[i].iconPath));
            pocketPanel.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = 
                LootManager.Instance.pocketItem[i].count.ToString();
        }
    }
}