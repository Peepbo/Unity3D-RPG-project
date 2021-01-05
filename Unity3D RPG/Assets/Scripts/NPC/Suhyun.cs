using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suhyun : MonoBehaviour
{

    private void Start()
    {
        JsonData.Instance.CheckJsonData();

        List<Achieve> _list = new List<Achieve>();

        _list = JsonData.Instance.LoadAchieve();

        for(int i = 0; i < _list.Count; i++)
        {
            Debug.Log(_list[i].Number);

            //if(i == 1)
            //{
            //    _list[i].Number = 100;
            //}
        }

        JsonData.Instance.AchieveSave(_list);
    }
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.T))
        //{
        //    EffectManager.Instance.EffectActive(2, Vector3.zero, Quaternion.identity);
        //}
    }
}
