using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    public GameObject[] dollyCam = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        var _player = GameObject.FindWithTag("Player").transform.position;

        GameObject _sellectCam = dollyCam[0];
        float _distance = Vector3.Distance(_player, dollyCam[0].transform.position);
        float _compare = 0;
        for (int i = 1; i < 4; i++)
        {
            _compare = Vector3.Distance(_player, dollyCam[i].transform.position);

            if(_compare < _distance)
            {
                _sellectCam = dollyCam[i];
                _distance = _compare;
            }
        }

        _sellectCam.SetActive(true);
    }
}
