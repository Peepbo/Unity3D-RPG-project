using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{   
    [SerializeField]
    private Player player;
    public MeshCollider meshCollider;
    bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            meshCollider.enabled = false;
            other.GetComponent<IDamagedState>().Damaged(10);
            Debug.Log("충돌했다");
        }
    }
}
