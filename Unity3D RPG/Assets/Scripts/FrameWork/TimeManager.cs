using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    protected TimeManager() { }

    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    void Update()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);


        if(Input.GetKeyDown(KeyCode.T))
        {
            DoSlowmotion();
        }
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Debug.Log("t");

        //    var obj = ObjectPool.SharedInstance.GetPooledObject("Untagged", 1);
        //    obj.transform.position = Vector3.zero;
        //    obj.transform.rotation = transform.rotation;

        //    obj.SetActive(true);

        //    StartCoroutine(coru(obj));
        //}

        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    Debug.Log("y");

        //    var obj = ObjectPool.SharedInstance.GetPooledObject("Untagged", 2);
        //    obj.transform.position = Vector3.zero;
        //    obj.transform.rotation = transform.rotation;

        //    obj.SetActive(true);

        //    StartCoroutine(coru(obj));
        //}

        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    Debug.Log("u");

        //    var obj = ObjectPool.SharedInstance.GetPooledObject("Untagged", 3);
        //    obj.transform.position = Vector3.zero;
        //    obj.transform.rotation = transform.rotation;

        //    obj.SetActive(true);

        //    StartCoroutine(coru(obj));
        //}
    }

    //IEnumerator coru(GameObject _obj)
    //{
    //    yield return new WaitForSeconds(2f);
    //    _obj.SetActive(false);
    //}

    public void DoSlowmotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
