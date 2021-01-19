using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class TalkNpc : MonoBehaviour
{
    public enum NPC
    {
        CHEST,
        TRAINER,
        SMITH,
        INSURANCE
    }
    public NPC npcName;
    public GameObject talkPanel;
    public Animator anim;

    [Header("CHEST")]
    public GameObject equipPanel;
    public GameObject chestPanel;

    [Header("TRAINER")]
    public GameObject growthPanel;
    //public GameObject characteristicPanel;

    [Header("SMITH")]
    public GameObject smithPanel;

    [Header("INSURANCE")]
    public GameObject insurancePanel;

    [Header("TALK INFO")]
    public Text btn2Name;
    public Text btn2Text;

    public Text btn3Name;
    public Text btn3Text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(npcName);
            if (npcName != NPC.CHEST) anim.SetTrigger("Talk");
            
            else
            {
                anim.SetBool("Talk", true);
                SoundManager.Instance.SFXPlay2D("Chest_Open", 0.75f);
            }

            talkPanel.SetActive(true);

            //01/04
            UiManager0.Instance.PanelOpen = true;
            //

            GameObject btn2 = talkPanel.transform.GetChild(0).gameObject;
            GameObject btn3 = talkPanel.transform.GetChild(1).gameObject;

            GameObject[] childObj = new GameObject[3];

            switch (npcName)
            {
                case NPC.CHEST://3가지
                    btn2.SetActive(false);
                    btn3.SetActive(true);

                    for(int i = 0; i < 3; i++)
                        childObj[i] = btn3.transform.GetChild(i).gameObject;

                    btn3Name.gameObject.SetActive(true);
                    btn3Text.gameObject.SetActive(true);

                    btn3Name.text = "창고";

                    string[] _texts = { "끼익", "끼이이익....", "끼익.." };
                    int _ran = Random.Range(0, _texts.Length);
                    btn3Text.text = _texts[_ran];

                    childObj[0].transform.GetChild(0).GetComponent<Text>().text = "장비 착용";
                    childObj[1].transform.GetChild(0).GetComponent<Text>().text = "아이템 확인";
                    

                    //addListener

                    //action0
                    ResetAndAddListener(childObj[0].GetComponent<Button>(), equipPanel);

                    //action1
                    ResetAndAddListener(childObj[1].GetComponent<Button>(), chestPanel);

                    //clickSound
                    ClickAddListener(childObj[2].GetComponent<Button>());
                    break;
                case NPC.TRAINER://3가지 -> 2가지
                    btn2.SetActive(true);
                    btn3.SetActive(false);

                    for (int i = 0; i < 2; i++)
                        childObj[i] = btn2.transform.GetChild(i).gameObject;

                    btn2Name.gameObject.SetActive(true);
                    btn2Text.gameObject.SetActive(true);

                    btn2Name.text = "트레이너";

                    string[] _texts2 = { "수련이 필요해보이군..", "훈련이 필요한가?", "너도 나처럼 될 수 있어" };
                    int _ran2 = Random.Range(0, _texts2.Length);
                    btn2Text.text = _texts2[_ran2];

                    childObj[0].GetComponentInChildren<Text>().text = "성장";
                    //childObj[1].transform.GetChild(0).GetComponent<Text>().text = "특성";

                    //action0
                    ResetAndAddListener(childObj[0].GetComponent<Button>(), growthPanel);

                    //clickSound
                    ClickAddListener(childObj[1].GetComponent<Button>());
                    //action1
                    //ResetAndAddListener(childObj[1].GetComponent<Button>(), characteristicPanel);

                    break;
                case NPC.SMITH://2가지
                    btn2.SetActive(true);
                    btn3.SetActive(false);

                    for (int i = 0; i < 2; i++)
                        childObj[i] = btn2.transform.GetChild(i).gameObject;

                    btn2Name.gameObject.SetActive(true);
                    btn2Text.gameObject.SetActive(true);

                    btn2Name.text = "대장장이";

                    string[] _texts3 = { "Take a look", "신상 무기 나왔습니다~", "기사님 한 번 둘러보세요" };
                    int _ran3 = Random.Range(0, _texts3.Length);
                    btn2Text.text = _texts3[_ran3];

                    //childObj[0] = btn2.transform.GetChild(0).gameObject;

                    childObj[0].GetComponentInChildren<Text>().text = "장비 제작";
                    ResetAndAddListener(childObj[0].GetComponent<Button>(), smithPanel);

                    ClickAddListener(childObj[1].GetComponent<Button>());

                    break;
                case NPC.INSURANCE://2가지
                    btn2.SetActive(true);
                    btn3.SetActive(false);

                    for (int i = 0; i < 2; i++)
                        childObj[i] = btn2.transform.GetChild(i).gameObject;
                    //childObj[0] = btn2.transform.GetChild(0).gameObject;

                    btn2Name.gameObject.SetActive(true);
                    btn2Text.gameObject.SetActive(true);

                    btn2Name.text = "보험사";

                    string[] _texts4 = { "혹시 보험 필요하지 않으세요?", "기사님께 필요한 보험이 있습니다!" };
                    int _ran4 = Random.Range(0, _texts4.Length);
                    btn2Text.text = _texts4[_ran4];

                    childObj[0].GetComponentInChildren<Text>().text = "보험 구매";
                    ResetAndAddListener(childObj[0].GetComponent<Button>(), insurancePanel);



                    ClickAddListener(childObj[1].GetComponent<Button>());
                    break;
            }

            //if(npcName != NPC.CHEST) SoundManager.Instance.SFXPlay2D("UI_NPCPopup");
            SoundManager.Instance.SFXPlay2D("UI_NPCPopup");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && npcName == NPC.CHEST)
        {
            anim.SetBool("Talk", false);
        }
    }

    void ResetAndAddListener(Button btn, GameObject openPanel)
    {
        btn.onClick.RemoveAllListeners();

        UnityAction _action = () =>
        {
            openPanel.SetActive(true);
            talkPanel.SetActive(false);
            UiManager0.Instance.PanelOpen = false;
            SoundManager.Instance.SFXPlay2D("UI_Popup");
            UI_ClickSound();

            btn2Name.gameObject.SetActive(false);
            btn2Text.gameObject.SetActive(false);
            btn3Name.gameObject.SetActive(false);
            btn3Text.gameObject.SetActive(false);
        };

        btn.onClick.AddListener(_action);
        
    }

    void ClickAddListener(Button btn)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(UI_ClickSound);

    }
    public void UI_ClickSound()
    {
        SoundManager.Instance.SFXPlay2D("UI_Click", 0.6f);
    }
}
