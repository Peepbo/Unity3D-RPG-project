using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public GameObject lootButton;
    public GameObject lootPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openLoot()
    {
        lootPanel.SetActive(true);
    }

    public void closeLoot()
    {
        lootPanel.SetActive(false);
    }
}
