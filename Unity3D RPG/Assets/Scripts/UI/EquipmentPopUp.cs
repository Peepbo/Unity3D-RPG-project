using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSVReader;
using CSVWrite;
using CSVSimpleReader;

public class EquipmentPopUp : MonoBehaviour
{
    public GameObject equipButton;
    public GameObject equipPanel;

    private const int WEAPON = 0, ARMOR = 1, ACCESSORY = 2, SHIELD = 3;

    public Text[] contents;
    public Image[] slots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenEquipment()
    {
        equipPanel.SetActive(true);
        equipButton.SetActive(false);
    }
    public void CloseEquipment()
    {
        equipPanel.SetActive(false);
        equipButton.SetActive(true);
    }
}
