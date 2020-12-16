using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPopUp : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject MenuIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuPop()
    {
         Time.timeScale = 0f; // 먼저 시간을 정지시킨다. (게임내 시간으로 게임 시간이 정지된다. 애니메이션 , 게임구동 관련 시간)
        // ↑ 이거 쓸지안쓸지 몰라서 일단 넣어놓았음.추후 수정 예정

         MenuPanel.SetActive(true);
         MenuIcon.SetActive(false);
    }

    public void MenuClose()
    {
        MenuPanel.SetActive(false);
        MenuIcon.SetActive(true);
    }
}
