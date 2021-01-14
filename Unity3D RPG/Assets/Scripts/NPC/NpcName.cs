using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcName : MonoBehaviour
{
    public string name;

    // Start is called before the first frame update
    void Update()
    {
        gameObject.transform.LookAt(Camera.main.transform.position);
        gameObject.transform.Rotate(0, 180, 0);
        gameObject.GetComponent<TextMesh>().text = "" + name;
    }
}
