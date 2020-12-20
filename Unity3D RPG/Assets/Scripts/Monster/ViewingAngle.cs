using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewingAngle : MonoBehaviour
{
    GameObject target;
    public float angle = 30f;
    public float radius = 5f;
    float cosValue;
    bool isCol;
    Vector3 direction;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        //Mathf.Deg2Rad : Degree to radius , (PI *2)/360과 같음
        cosValue = Mathf.Cos(Mathf.Deg2Rad *(angle/2));
        direction= target.transform.position - transform.position;

        float _distance = Vector3.Distance(target.transform.position, transform.position);
        

        //타겟이 인식 가능한 거리에 있을 때
        if (_distance < radius)
        {
            if (Vector3.Dot(direction.normalized, transform.forward) > cosValue)
            {
                isCol = true;
            }
            else isCol = false;
        }
        else
        {
            isCol = false;
        }


    }
}
