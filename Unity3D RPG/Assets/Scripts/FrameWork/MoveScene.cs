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
                if (sceneName == "TownScene")
                {
                    PlayerData.Instance.isReturn = true;

                    LootManager.Instance.Delivery();
                    DungeonMng.Instance.ResetStage();
                    if (LoadingSceneController.Instance.loadSceneName == "Tutorial")
                    {
                        PlayerData.Instance.isIntro = true;

                    }
                }

                SoundManager.Instance.SFXPlay2D("UI_Warp",0.3f);
                LoadingSceneController.Instance.LoadScene(sceneName);
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (sceneName == "TownScene")
        {
            PlayerData.Instance.isReturn = true;
            
            if (LoadingSceneController.Instance.loadSceneName != "")
            {
                DungeonMng.Instance.ClearCount();
                DungeonMng.Instance.ResetStage();

                LootManager.Instance.Delivery();
                if(LoadingSceneController.Instance.loadSceneName  == "Tutorial")
                {
                    PlayerData.Instance.isIntro = true;

                }
            }
        }

        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
