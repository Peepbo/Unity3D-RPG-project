using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMng : MonoBehaviour
{
    bool isStart = false;
    BoxCollider col;

    public Chat chat;

    public bool[] questCheck = new bool[7];

    //shamen
    public GameObject monster;

    #region begin
    private void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isStart = true;
            col.enabled = false;
            chat.NextQuest();

            monster.SetActive(true);
        }
    }
    #endregion
}
