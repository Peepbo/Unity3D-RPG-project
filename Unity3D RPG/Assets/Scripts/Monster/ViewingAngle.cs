using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ViewingAngle : MonoBehaviour
{
    GameObject target;

    float angle;
    float radius;

    float findRadius;


    float cosValue;
    bool isCol;

    bool isFind = false;
    Vector3 direction;

    public float getviewAngleValue() {return angle; }

    //if target is in the viewing angle, return true;  
    public bool FoundTarget(GameObject newTarget, float findRange, float checkAngle)
    {
       
        target = newTarget;
        findRadius = findRange;
        angle = checkAngle;

        Vector3 _transform = transform.position;
        Vector3 _targetTran = newTarget.transform.position;

        _targetTran.y= 0;
        _transform.y = 0;
       

        //Mathf.Deg2Rad : Degree to radius , (PI *2)/360과 같음
        cosValue = Mathf.Cos(Mathf.Deg2Rad * angle / 2);

        // Vector3 _range = target.transform.position - transform.position;
        Vector3 _range = _targetTran - _transform;

        if (_range.magnitude < findRadius)
        {
            if (Vector3.Dot(_range.normalized, transform.forward) > cosValue)
            {
                isFind = true;
            }
            else isFind = false;
        }
        else isFind = false;

        return isFind;
    }


    public bool ableToDamage()
    {
        ////Mathf.Deg2Rad : Degree to radius , (PI *2)/360과 같음
        //cosValue = Mathf.Cos(Mathf.Deg2Rad * (angle / 2));

        //direction = newTarget.transform.position - transform.position;

        //float _distance = Vector3.Distance(transform.position, newTarget.transform.position);

        ////타겟이 인식 가능한 거리에 있을 때
        //if (_distance < radius)
        //{
        //    //만약 맵이 평면이라면 target과 현재 transfrom y를 0으로 만들어주기
        //    if (Vector3.Dot(direction.normalized, transform.forward) > cosValue)
        //    {
        //        isCol = true;
        //    }
        //    else
        //    {
        //        isCol = false;
        //    }
        //}
        //else
        //{
        //    isCol = false;
        //}
        return isCol;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //if (isCol)
        //    Handles.color = Color.red;
        //else
        //    Handles.color = Color.yellow;

        //Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angle / 2, radius);
        //Handles.DrawSolidArc(transform.position, Vector3.down, transform.forward, angle / 2, radius);


        //if (isFind)
        //{
        //    Handles.color = Color.red;
        //}
        //else Handles.color = Color.yellow;

        //Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angle / 2, findRadius);
        //Handles.DrawSolidArc(transform.position, Vector3.down, transform.forward, angle / 2, findRadius);

        //if (isAttackable)
        //{
        //    Handles.color = Color.red;
        //}
        //else Handles.color = Color.yellow;

        //Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angle / 2, attackRadius);
        //Handles.DrawSolidArc(transform.position, Vector3.down, transform.forward, angle / 2, attackRadius);

    }
#endif
}