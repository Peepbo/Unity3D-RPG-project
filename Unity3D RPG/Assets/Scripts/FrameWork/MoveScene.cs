using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    public string sceneName;
    public bool isTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            if (other.tag == "Player")
            {
                LoadingSceneController.Instance.LoadScene(sceneName);
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
