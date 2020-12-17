using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPopUp : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject menuIcon;
    public GameObject achievementPanel;

    public GameObject shopIcon;
    public GameObject shopPanel;

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

         menuPanel.SetActive(true);
         menuIcon.SetActive(false);
    }

    public void MenuClose()
    {
        menuPanel.SetActive(false);
        menuIcon.SetActive(true);
    }

    public void AchievementPop()
    {
        achievementPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void AchievementClose()
    {
        achievementPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShopPop()
    {
        shopPanel.SetActive(true);
        shopIcon.SetActive(false);
    }

    public void ShopClose()
    {
        shopPanel.SetActive(false);
        shopIcon.SetActive(true);
    }
}
