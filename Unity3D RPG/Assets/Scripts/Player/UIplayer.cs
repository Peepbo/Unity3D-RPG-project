using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIplayer : MonoBehaviour
{
    public Image hpBar;
    public Image staminaBar;
    Player player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        HpBarControl();
        StaminaBarControl();
    }

    public void HpBarControl()
    {

        hpBar.fillAmount = (float)player.hp / (float)player.maxHp;
        //Debug.Log((float)player.hp / (float)player.maxHp + "Hp야 ~~");

    }
    public void StaminaBarControl()
    {

        staminaBar.fillAmount = (float)player.stamina / (float)player.maxStamina;
    }
}
