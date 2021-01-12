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
                //if(LoadingSceneController.Instance.loadSceneName == "TownScene")
                //{
                //    LootManager.Instance.ClearPocketData();
                //}
                if (sceneName == "TownScene")
                {
                    PlayerData.Instance.isReturn = true;
                    LootManager.Instance.Delivery();
                }

                if (LoadingSceneController.Instance.loadSceneName == "TownScene")
                {
                    Debug.Log("clear loot");
                    LootManager.Instance.ClearPocketData();
                }

                LoadingSceneController.Instance.LoadScene(sceneName);
               
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (sceneName == "TownScene")
        {
            PlayerData.Instance.isReturn = true;

            if(LoadingSceneController.Instance.loadSceneName != "")
            {
                //Debug.Log(LoadingSceneController.Instance.loadSceneName);
                DungeonMng.Instance.ClearCount();
                LootManager.Instance.Delivery();
            }
        }

        if(LoadingSceneController.Instance.loadSceneName == "TownScene")
        {
            Debug.Log("clear loot");
            LootManager.Instance.ClearPocketData();
        }

        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
