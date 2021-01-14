using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        if (myImage.color.a > 0.3f)
        {
            myImage.color = Color.Lerp(myImage.color, sampleColor, Time.realtimeSinceStartup * 0.001f);
            myText.color = Color.Lerp(myText.color, textColor, Time.realtimeSinceStartup * 0.001f);
        }
        else gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().name == "TownScene")
        {
            this.gameObject.transform.GetChild(0).GetComponent <Text>().text = "마을";
        }
       else
        {
            this.gameObject.transform.GetChild(0).GetComponent<Text>().text = "던전";
        }
    }
}
