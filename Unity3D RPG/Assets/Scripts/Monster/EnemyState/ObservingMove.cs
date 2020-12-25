using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservingMove : MonoBehaviour, IMoveAble
{
    CharacterController controller;
    Vector3 spawnPos;
    Vector3 randomDirection;
    float speed;
    float observeRange;
    float time;
    bool isRangeOver;
    bool isObserve;

    public void initVariable(CharacterController cc, Vector3 spawnPosition, Vector3 direction, float moveSpeed, float moveRange)
    {
        controller = cc;
        speed = moveSpeed;
        observeRange = moveRange;
        spawnPos = spawnPosition;
        randomDirection = direction;
    }
    
    public bool getIsObserve() {return isObserve; }
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
        float _distance = Vector3.Distance(spawnPos, transform.position);
        if (_distance > observeRange)
        {
            //방향 체크할 radar 가져오기..
            Debug.Log("범위를 벗어남");
            isRangeOver = true;
            
        
        }
        else
        {
            //반복해서 randodirection받는 건 밖에서 처리하기
            controller.Move(randomDirection * speed * Time.deltaTime);
        }
    }
}
