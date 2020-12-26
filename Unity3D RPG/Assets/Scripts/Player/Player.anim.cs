using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

partial class Player
{
    private Animator animator;

    // animation name 애니메이션 이름 넣고 여기에 이름추가
    const string stateIdle = "Idle";
    const string stateWalk = "Walk";
    const string stateRun = "Run";

    string currentState;

    void AnimStart()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void ChangeAnimation(string newState)
    {
        if(currentState== newState)
        {
            return;
        }
        animator.Play(newState);

        currentState = newState;

    }


}
