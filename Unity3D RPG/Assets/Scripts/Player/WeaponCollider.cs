using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public GameObject particle;
    public GameObject[] effect;
    [SerializeField]
    private Player player;
    public BoxCollider meshCollider;
    bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        meshCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<IDamagedState>().Damaged((int)player.realAtk);
            Instantiate(effect[0], other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), transform.rotation);
            Instantiate(effect[1], other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), transform.rotation);
        }
    }
}
