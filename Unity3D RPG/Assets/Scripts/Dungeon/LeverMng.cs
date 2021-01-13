using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverMng : MonoBehaviour
{
    public GameObject[] lever = new GameObject[2];
    public int activeLever = 0;
    bool oneTime = false;

    public GameObject door;
    public GameObject[] cam = new GameObject[2];

    public float maxDistance;

    GameObject target;

    public void TurnOn()
    {
        activeLever++;
        target = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        if(activeLever == 2 && !oneTime)
        {
            oneTime = true;

            var _cam = CheckDistance();

            StartCoroutine(CameraEvent(_cam));
        }
    }

    IEnumerator CameraEvent(GameObject eventCam)
    {
        eventCam.SetActive(true);

        var _child = 
            eventCam.transform.GetChild(0).GetComponent<Cinemachine.CinemachineDollyCart>();

        _child.m_Position = 0;
        Time.timeScale = 0f;
        float _checkDistance = -1;
        while (_child.m_Position != _checkDistance)
        {
            _checkDistance = _child.m_Position;
            _child.m_Position += Time.realtimeSinceStartup * 0.004f;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        door.GetComponent<Animator>().SetBool("Open", true);

        yield return new WaitForSecondsRealtime(2f);

        eventCam.SetActive(false);
        Time.timeScale = 1f;
    }

    GameObject CheckDistance()
    {
        float _dis0, _dis1;

        _dis0 = (lever[0].transform.position - target.transform.position).magnitude;
        _dis1 = (lever[1].transform.position - target.transform.position).magnitude;

        GameObject _selecCam;

        if (_dis0 > _dis1)
        {
            _selecCam = cam[0];
        }
        else _selecCam = cam[1];

        return _selecCam;
    }
}
