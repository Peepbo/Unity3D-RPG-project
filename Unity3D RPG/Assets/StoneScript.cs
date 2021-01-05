using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    Rigidbody rigid;
    GameObject effect;
    Renderer mat;

    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        effect = transform.GetChild(0).gameObject;
        
        mat = gameObject.GetComponent<Renderer>();
        Color _color = mat.material.color;

        mat.material.color = _color;
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.down * 2.5f, Color.red);

        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 2.5f))
        {
            //Debug.Log(_hit.transform.name);
            effect.SetActive(true);
        }

        else rigid.AddForce(Vector3.down * 9.78f);
    }

}
