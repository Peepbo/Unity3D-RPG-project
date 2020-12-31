using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Guard : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    Player player;
    bool isPress;
    private void Awake()
    {
        player = GameObject.Find("MainPlayer").GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if( isPress)
        {
            player.comboAtk.animator.SetBool("isGuard", true);
            

        }
        else
        {
            player.comboAtk.animator.SetBool("isGuard", false);
            if(player.comboAtk.animator.GetBool("isGuardHit") == false)
            {
                player.isGuard = false;
            }

        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!player.isCri)
        {
            player.state = Player.PlayerState.GUARD;
            isPress = true;
            player.isGuard = true;

            player.comboAtk.ComboReset();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.state = Player.PlayerState.IDLE;
        isPress = false;
        
    }
}
