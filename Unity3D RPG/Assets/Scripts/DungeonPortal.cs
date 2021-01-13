using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPortal : MonoBehaviour
{
    int myStage = 0;
    // Start is called before the first frame update
    void Start()
    {
        myStage = DungeonMng.Instance.stage;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (DungeonMng.Instance.stage != 0) // 마을이 아니면?
            {
                List<Achieve> _list = new List<Achieve>(JsonData.Instance.LoadAchieve());

                //5~10 monster kill
                for(int i = 5; i <= 10; i++)
                {
                    _list[i].Number += DungeonMng.Instance.killCount;
                    DungeonMng.Instance.ClearCount();
                }
            }

            DungeonMng.Instance.stage++;

            if (myStage != 3)
            {
                LoadingSceneController.Instance.LoadScene(GetRandomMap());
            }

            else//boss room
            {
                LoadingSceneController.Instance.LoadScene("BossRoom(light bake)");
            }
            
            //sound
            SoundManager.Instance.SFXPlay2D("UI_Warp", 0.3f);
        }
    }

    string GetRandomMap()
    {
        string[] _scenes = {
                    "Dungeon 1(light bake)",
                    "Dungeon 2(light bake)",
                    "D 1-3",
                    "D 1-4" };

        int _ran = Random.Range(0, 4);

        return _scenes[_ran];
    }
}