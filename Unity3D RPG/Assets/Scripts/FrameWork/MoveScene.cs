using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    public bool isTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            if (other.tag == "Player")
            {
                LoadingSceneController.Instance.LoadScene("Dungeon 1");
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
