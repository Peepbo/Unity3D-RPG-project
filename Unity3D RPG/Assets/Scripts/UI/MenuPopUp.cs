using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPopUp : MonoBehaviour
{
    public GameObject quitPanel;

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) // 안드로이드에서
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // 뒤로가기 버튼 누르면
            {
                Time.timeScale = 0f; // 시간정지
                quitPanel.SetActive(true);
            }
        }
    }

    public void MenuPop()
    {
        Time.timeScale = 0f; // 먼저 시간을 정지시킨다. (게임내 시간으로 게임 시간이 정지된다. 애니메이션 , 게임구동 관련 시간)
        // ↑ 이거 쓸지안쓸지 몰라서 일단 넣어놓았음.추후 수정 예정
    }

    public void MenuClose()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
