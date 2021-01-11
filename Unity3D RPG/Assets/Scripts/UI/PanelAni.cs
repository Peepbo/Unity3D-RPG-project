using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAni : MonoBehaviour
{
    Vector3 Nextposition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Up()
    {
        Nextposition = new Vector3(transform.position.x, transform.position.y + Screen.height, 0);
        StartCoroutine(Movecamera());

    }

    public void Down()
    {
        this.transform.position = new Vector2(800, -460);
    }

    IEnumerator Movecamera()
    {
        yield return new WaitForSeconds(0.2f);
        while (transform.position != Nextposition)
        {
            transform.position = Vector3.Lerp(transform.position, Nextposition, 8f * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
