using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


partial class Player
{
    float hAxis;
    float vAxis;
    public float speed;
    Rigidbody rigid;
    Vector3 moveVec;

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        
    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis);
        if (hAxis != 0 || vAxis != 0)
        {
            transform.Translate(moveVec * speed * Time.deltaTime);
        }
    }
    void Turn()     
    {
       //나중에 

    }
}

