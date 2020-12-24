using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeState 
{
    void Idle();
    void Move();
    void Attack();
    void Damaged();

    void Die();
}
