using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTable : StateMachineBehaviour
{
    Manager table;

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }


    //override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)

	{
        table = FindObjectOfType<Manager>();


		if(table.twins && animator.gameObject.name.Contains("(1)"))
			return;
        animator.gameObject.SetActive(false);
		
		if(table.twins)
			table.GOs[table.currIndx+3].SetActive(false);
        
        table.currIndx++;
        table.anim.SetInteger("n", table.currIndx);
		if (!table.twins && table.currIndx == 3)
			return;
		if (table.twins && table.currIndx > table.GOs.Length/3)
        {
            //table.gameObject.GetComponent<Animator>().enabled = false;
            //table.freez = true;
            return;
        }
        table.GOs[table.currIndx].SetActive(true);
		
		if(table.twins)
			table.GOs[table.currIndx+3].SetActive(true);


        
    }

}
