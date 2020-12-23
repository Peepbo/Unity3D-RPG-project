using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

partial class Player
  {
    public Image hpBar;
    public Image staminaBar;


    void PlayerUiUpdate()
    {
        HpBarControl();
        StaminaBarControl();
    }

   
    public void HpBarControl()
    {
         
         hpBar.fillAmount = (float)hp / (float)maxHp;
         
    }
    public void StaminaBarControl()
    {
        
        staminaBar.fillAmount = (float)stamina / (float)maxStamina;
    }
}

