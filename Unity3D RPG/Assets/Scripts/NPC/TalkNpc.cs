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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(npcName);
            if (npcName != NPC.CHEST) anim.SetTrigger("Talk");

            else anim.SetBool("Talk", true);

            talkPanel.SetActive(true);

            //01/04
            UiManager.Instance.PanelOpen = true;
            //

            GameObject btn2 = talkPanel.transform.GetChild(0).gameObject;
            GameObject btn3 = talkPanel.transform.GetChild(1).gameObject;

            GameObject[] childObj = new GameObject[2];

            switch (npcName)
            {
                case NPC.CHEST://3가지
                    btn2.SetActive(false);
                    btn3.SetActive(true);

                    for(int i = 0; i < 2; i++)
                        childObj[i] = btn3.transform.GetChild(i).gameObject;

                    childObj[0].transform.GetChild(0).GetComponent<Text>().text = "장비 착용";
                    childObj[1].transform.GetChild(0).GetComponent<Text>().text = "아이템 확인";


                    //addListener

                    //action0
                    ResetAndAddListener(childObj[0].GetComponent<Button>(), equipPanel);

                    //action1
                    ResetAndAddListener(childObj[1].GetComponent<Button>(), chestPanel);

                    break;
                case NPC.TRAINER://3가지 -> 2가지
                    btn2.SetActive(true);
                    btn3.SetActive(false);

                    //for (int i = 0; i < 2; i++)
                    //    childObj[i] = btn3.transform.GetChild(i).gameObject;

                    childObj[0] = btn2.transform.GetChild(0).gameObject;

                    childObj[0].GetComponentInChildren<Text>().text = "성장";
                    //childObj[1].transform.GetChild(0).GetComponent<Text>().text = "특성";

                    //action0
                    ResetAndAddListener(childObj[0].GetComponent<Button>(), growthPanel);

                    //action1
                    //ResetAndAddListener(childObj[1].GetComponent<Button>(), characteristicPanel);

                    break;
                case NPC.SMITH://2가지
                    btn2.SetActive(true);
                    btn3.SetActive(false);

                    childObj[0] = btn2.transform.GetChild(0).gameObject;
                    
                    childObj[0].GetComponentInChildren<Text>().text = "장비 제작";
                    ResetAndAddListener(childObj[0].GetComponent<Button>(), smithPanel);
                    break;
                case NPC.INSURANCE://2가지
                    btn2.SetActive(true);
                    btn3.SetActive(false);

                    childObj[0] = btn2.transform.GetChild(0).gameObject;

                    childObj[0].GetComponentInChildren<Text>().text = "보험 구매";
                    ResetAndAddListener(childObj[0].GetComponent<Button>(), insurancePanel);
                    break;
            }
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
            UiManager.Instance.PanelOpen = false;
        };

        btn.onClick.AddListener(_action);
    }
}
