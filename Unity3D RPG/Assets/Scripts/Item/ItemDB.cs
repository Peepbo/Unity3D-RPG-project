using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemDB : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1YuynEfJHxImtJK0brB68n2lLriSeY-y_O9WkFxoI1Wg/export?format=tsv";
    IEnumerator Start()
    {
        UnityWebRequest webLink = UnityWebRequest.Get(URL);
        yield return webLink.SendWebRequest();

        string data = webLink.downloadHandler.text;
        print(data);
    }

    // Update is called once per frame 
    
}
