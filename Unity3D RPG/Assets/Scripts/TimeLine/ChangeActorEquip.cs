using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeActorEquip : MonoBehaviour
{
    public GameObject Shield;
    public GameObject Weapon;
    int currenEquipment;

    private void Start()
    {
        currenEquipment = PlayerData.Instance.myEquipment[1];

        if (currenEquipment == -1)
        {
            Weapon.gameObject.SetActive(false);
            Shield.gameObject.SetActive(false);
        }

        else
        {
            Weapon.transform.GetChild(currenEquipment).gameObject.SetActive(true);

            if (currenEquipment <= 10)
                Shield.transform.GetChild(currenEquipment).gameObject.SetActive(true);
        }

    }

}
