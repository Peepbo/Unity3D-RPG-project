using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private MoveAble moveable;
    private AttackAble attackAble;

    public Goblin (MoveAble move, AttackAble attack)
    {
        this.moveable = move;
        this.attackAble = attack;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveable.move();
        attackAble.attack();
    }
}
