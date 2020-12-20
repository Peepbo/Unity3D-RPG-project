using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DropItem : MonoBehaviour
{
    public GameObject BakedApple;
    public string items;

    // Update is called once per frame
    void Update()
    {

    }

    public void dropItem(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            float ranX = UnityEngine.Random.Range(-1f, 1f);
            float ranZ = UnityEngine.Random.Range(-1f, 1f);
            Vector3 _ranDir = new Vector3(ranX * 3f, 0, ranZ * 3f);

            Instantiate(BakedApple, transform.position + _ranDir, Quaternion.identity);
        }
    }

    public void dropItem2()
    {
        //string[] _item = items.Split(new char[] {','});

        //items = "bakedApple,armour,sword"

        //_item[0] = "bakedApple"
        //_item[1] = "armour"
        //_item[2] = "sword"
        //for(int i = 0; i < _item.Length; i++)
        //{
        //    // Assets/Prefabs/Enemys/BakedApple

        //    //Resources.Load<Sprite>("Sprites/sprite01");
        //    Debug.Log(_item[i]);
        //}

        //OldNecklace,OldArmour,OldSword

        string[] data = items.Split(new char[] { ',' });

        //0,1,2

        int ranNum = Random.Range(0, 3);



        string _prefName = CSVData.Instance.find(data[ranNum]).prefabName;

        string path = "Assets/Prefabs/" + _prefName + ".prefab";

        Debug.Log(path);

        //GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        //Instantiate(obj, transform.position, Quaternion.identity);
    }

    private void Start()
    {
      //  dropItem2();
    }
}