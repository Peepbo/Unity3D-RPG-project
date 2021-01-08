using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiChanger : MonoBehaviour
{
    public GameObject TownUi;
    public GameObject DungeonUi;

    private void Awake()
    {
        if (LoadingSceneController.Instance.loadSceneName == "TownScene")
            TownUi.SetActive(true);
        else 
            DungeonUi.SetActive(true);
    }
}
