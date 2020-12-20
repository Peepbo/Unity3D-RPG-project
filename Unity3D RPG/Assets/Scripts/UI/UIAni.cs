using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAni : MonoBehaviour
{
    Image myImage;
    public Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color sampleColor = myImage.color;
        sampleColor.a = 0;
        Color textColor = myText.color;
        textColor.a = 0;

        if (myImage.color.a > 0.05f)
        {
            myImage.color = Color.Lerp(myImage.color, sampleColor, Time.realtimeSinceStartup * 0.05f);
            myText.color = Color.Lerp(myText.color, textColor, Time.realtimeSinceStartup * 0.05f);
        }
        else gameObject.SetActive(false);
    }
}
