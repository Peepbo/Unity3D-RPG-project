using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Potion : MonoBehaviour, IPointerDownHandler
{
    Player player;
    [SerializeField] GameObject button;
    Text potionNumTxt;
    private void Awake()
    {
        player = GameObject.Find("MainPlayer").GetComponent<Player>();
        potionNumTxt = button.transform.GetChild(1).GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        potionNumTxt.text = "1";
    }

    
}
