using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSwitch : MonoBehaviour
{
    public BossDB boss;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.tag == "Player")
        {
            boss.start = true;
            boss.state = BossState.COMBATIDLE;
            Destroy(gameObject);
        }
     }


}
