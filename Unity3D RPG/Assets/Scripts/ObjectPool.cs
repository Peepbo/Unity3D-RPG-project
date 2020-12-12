using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    //오브젝트를 담아둘 리스트
    public List<GameObject> pooledObjects;

    //리스트에 담아둘 오브젝트(프리펩)
    public GameObject objectToPool;

    //풀에 넣을 오브젝트 최대 개수
    public int amountToPool;

    private void Awake()
    {
        //다른 스크립트가 컴포넌트를 가져 오지 않고도
        //액세스 할 수 있도록 함
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();

        for(int i = 0; i < amountToPool; i++)
        {
            //프리펩의 정보를 가지고 복제 후, obj에 할당
            GameObject obj = Instantiate(objectToPool);

            //화면 상 보이면 안되므로 Active를 false
            obj.SetActive(false);

            //List에 Add해줌
            pooledObjects.Add(obj);

            //몇 번? amountToPool 만큼 
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            //만약 i번째의 object가 Hierarchy에서 활성화가 아니면?
            if(pooledObjects[i].activeInHierarchy == false)
            {
                //그 오브젝트를 return 한다
                return pooledObjects[i];
            }
        }

        //여기까지 오면 위에 for문에서 return이 되지 않았다는 의미이며
        //null을 리턴한다
        return null;
    }
}
