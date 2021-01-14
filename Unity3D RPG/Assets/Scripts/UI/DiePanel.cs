using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        if (PlayerData.Instance.player.isDie == true)
        {
            this.gameObject.SetActive(true);
        }
    }
}
