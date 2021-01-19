using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int objectIndex;

    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    public List<ObjectPoolItem> itemsToPool;

    //public GameObject objectToPool;

    //public int amountToPool;

    //public bool shouldExpand = true;

    public List<GameObject> pooledObjects;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();

        int _poolCount = itemsToPool.Count;
        for (int i = 0; i < _poolCount; i++)
        {
            ObjectPoolItem _item = itemsToPool[i];

            int _amount = _item.amountToPool;
            for (int j = 0; j < _amount; j++)
            {
                GameObject _obj = Instantiate(_item.objectToPool);

                if (_item.objectIndex != 0) _obj.AddComponent<ObjectIndex>().index = _item.objectIndex;

                _obj.transform.parent = transform;
                _obj.SetActive(false);
                pooledObjects.Add(_obj);
            }
        }

        //foreach (ObjectPoolItem item in itemsToPool)
        //{
        //    for (int i = 0; i < item.amountToPool; i++)
        //    {
        //        GameObject _obj = Instantiate(item.objectToPool);

        //        if (item.objectIndex != 0) _obj.AddComponent<ObjectIndex>().index = item.objectIndex;

        //        _obj.transform.parent = transform;
        //        _obj.SetActive(false);
        //        pooledObjects.Add(_obj);
        //    }
        //}
    }

    public GameObject GetPooledObject(string tag, int index = 0)
    {
        //풀에 존재하는 오브젝트 만큼 for문을 돈다
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //그 오브젝트가 Hierarchy창에서 활성화가 아니며,
            //내가 매개변수로 입력한 tag와 그 오브젝트의 tag가 같을 때
            if (pooledObjects[i].activeInHierarchy == false &&
                pooledObjects[i].tag.Equals(tag))
            {
                if (index != 0)
                {
                    if (pooledObjects[i].GetComponent<ObjectIndex>().index != index)
                        continue;
                }

                //그 오브젝트를 return
                return pooledObjects[i];
            }
        }

        //여기로 온 이유

        //1. tag를 실수로 잘못 입력했을 때 -> 밑에 foreach로 들어가도 맞는 tag가 없어서 결국 return null로 된다..

        int _poolCount = itemsToPool.Count;
        for (int i = 0; i < _poolCount; i++)
        {
            ObjectPoolItem _item = itemsToPool[i];

            if (_item.objectToPool.tag.Equals(tag))
            {

                //해당 프리펩의 확장성이 true?
                if (_item.shouldExpand == true)
                {
                    //생성 후
                    GameObject _obj = Instantiate(_item.objectToPool);
                    _obj.transform.parent = transform;

                    if (_item.objectIndex != 0) _obj.AddComponent<ObjectIndex>().index = _item.objectIndex;

                    _obj.SetActive(false);
                    pooledObjects.Add(_obj);

                    //반환
                    return _obj;
                }
            }
        }

        ////2. 가져오려는 오브젝트가 다 활성화 상태일 때
        //foreach (ObjectPoolItem item in itemsToPool)
        //{

        //    //우리가 종류별로 만들어 놓은 오브젝트(프리펩)의 태그랑 내가 매개변수로 넣어준 태그를 검사한다.
        //    if (item.objectToPool.tag.Equals(tag))
        //    {

        //        //해당 프리펩의 확장성이 true?
        //        if (item.shouldExpand == true)
        //        {
        //            //생성 후
        //            GameObject _obj = Instantiate(item.objectToPool);

        //            if (item.objectIndex != 0) _obj.AddComponent<ObjectIndex>().index = item.objectIndex;

        //            _obj.SetActive(false);
        //            pooledObjects.Add(_obj);

        //            //반환
        //            return _obj;
        //        }
        //    }
        //}

        return null;
    }
}
