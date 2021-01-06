using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suhyun : MonoBehaviour
{
    public int me = -1;
    public List<int> test = new List<int>();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerData.Instance.SaveChest(1);
            PlayerData.Instance.SaveChest(2);
        }
    }

    void prt()
    {
        for(int i = 0; i < test.Count; i++)
        {
            Debug.Log(test[i]);
        }
    }
}
