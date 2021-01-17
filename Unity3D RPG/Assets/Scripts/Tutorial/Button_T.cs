using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_T : MonoBehaviour, IDamagedState
{
    public TutorialMng tm;
    public GameObject portal;
    public GameObject effect;

    public void Damaged(int value)
    {
        if(tm.questNumber == 5)
        {
            gameObject.tag = "Untagged";
            tm.ChangeQuest();

            portal.SetActive(true);
            effect.SetActive(false);
        }
    }
}
