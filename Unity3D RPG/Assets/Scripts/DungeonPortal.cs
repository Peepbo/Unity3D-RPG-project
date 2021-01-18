using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPortal : MonoBehaviour
{
    bool isFirst = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isFirst)
        {
            isFirst = true;

            if (DungeonMng.Instance.stage != 0) // 마을이 아니면?
            {
                List<Achieve> _list = new List<Achieve>(JsonData.Instance.LoadAchieve());

                //5~10 monster kill
                for(int i = 5; i <= 10; i++)
                {
                    _list[i].Number += DungeonMng.Instance.killCount;
                }
                DungeonMng.Instance.ClearCount();
                JsonData.Instance.AchieveSave(_list);
            }

            DungeonMng.Instance.stage++;
            Debug.Log(DungeonMng.Instance.stage);

            if (DungeonMng.Instance.stage < 3)
            {
                LoadingSceneController.Instance.LoadScene(GetRandomMap());
            }

            else//boss room
            {
                LoadingSceneController.Instance.LoadScene("BossRoom(light bake)");
            }
            
            //sound
            SoundManager.Instance.SFXPlay2D("UI_Warp");
            //Debug.Log("warp");
        }
    }

    string GetRandomMap()
    {
        string[] _scenes = {
                    "Dungeon 1(light bake)",
                    "Dungeon 2(light bake)",
                    "D 1-3",
                    "D 1-4" };


        List<int> _list = new List<int>();
        for(int i = 0; i < 4; i++)
        {
            if (i != DungeonMng.Instance.playMap) _list.Add(i);
        }

        int _ran = Random.Range(0, _list.Count);

        int _index = _list[_ran];
        DungeonMng.Instance.playMap = _index;

        return _scenes[_index];
    }
}