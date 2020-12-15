using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class EnemyMove : MonoBehaviour
{
    CharacterController controller;
    GameObject target;
    Vector3 h, v;
  
    [Range(1,5)]
    public float speed;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position-transform.position;
        direction.Normalize();
        transform.LookAt(target.transform);

        if (Vector3.Distance(transform.position, target.transform.position) >= 5.0f)
            controller.Move(direction * speed * Time.deltaTime);

    }

}
