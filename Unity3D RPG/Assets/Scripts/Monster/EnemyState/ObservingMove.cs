using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservingMove : MonoBehaviour, IMoveAble
{
    CharacterController controller;
    Vector3 spawnPos;
    Vector3 randomDirection;
    public Transform pivotCenter;
    public Transform radar;

    public float speed;
    public float observeRange;
    public float time;

    public int action;

    public bool isRangeOver;
    public bool isObserve;

    public void initVariable(CharacterController cc, Vector3 spawnPosition, Vector3 direction, float moveSpeed, float moveRange)
    {
        controller = cc;
        speed = moveSpeed;
        observeRange = moveRange;
        spawnPos = spawnPosition;
        randomDirection = direction;
        isObserve = true;
        isRangeOver = false;

        //Range over일 때,  random Direction 설정 담당
        pivotCenter = transform.GetChild(1); //transform.Find("checkPivot");
        radar = pivotCenter.GetChild(0);//transform.Find("rader");
    }

    public int getAction() { return action; }
    public bool getIsObserve() { return isObserve; }
    public bool getIsRangeOver() { return isRangeOver; }

    public void setIsObserve(bool isObserving)
    {
        isObserve = isObserving;
    }
    public void setIsRangeOver(bool _isRangeOver)
    {
        isRangeOver = _isRangeOver;
    }
    public void setRandomDirection(Vector3 direction)
    {
        randomDirection = direction;
    }



    public void move()
    {
        if (!isObserve) return;

        time -= Time.deltaTime;

        float _distance = Vector3.Distance(spawnPos, transform.position);

        if (_distance < observeRange)
        {
            isRangeOver = false;
        }

        else
        {
            isRangeOver = true;
        }


        if (!isRangeOver)
        {
            //observe Range 안에 있으면 

            if (time < 0)
            {
                action = Random.Range(0, 2);
                time = Random.Range(2f, 5f);

                if (action == 1)
                {
                    //randomDirection은 observingMove 사용하는 클래스에서 입력받기
                    transform.forward = randomDirection;
                }
                else
                {
                    //action 0이면 움직이지 않고 생각하게 만들기
                    time = Random.Range(1f, 4f);
                }
            }


        }
        else
        {
            //observeRange에 닿으면 어느 방향으로 갈지 체크해서 그 방향으로 가도록 해주기
           // Debug.Log("범위를 벗어남");

            //isRangeOver = true;

            time = 3f;

            float[] _angle = { 0, 60, 120, 180, 240, 300 };
            // List<float> _angle = new List<float>(new float[] {0,60,120,180,240,300 });
            // list로 받아서 계산하는방법???? ->물어보기

            List<float> _newDirection = new List<float>();


            for (int i = 0; i < 6; i++)
            {
                pivotCenter.rotation = Quaternion.Euler(pivotCenter.rotation.x, _angle[i], pivotCenter.rotation.z);

                if (Vector3.Distance(radar.position, spawnPos) < observeRange)
                {
                    _newDirection.Add(_angle[i]);

                }
            }

            int _index = Random.Range(0, _newDirection.Count);
            transform.rotation = Quaternion.Euler(0, _newDirection[_index], 0);

            isRangeOver = false;
        }


        if (action != 0 && !isRangeOver)
            controller.Move(transform.forward * speed * Time.deltaTime);
    }

}