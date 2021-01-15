using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAppear : MonoBehaviour
{
    public GameObject obj;
    public GameObject effect;
    void Start() { StartCoroutine(Appear()); }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(1f);
        effect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(true);
    }
}
