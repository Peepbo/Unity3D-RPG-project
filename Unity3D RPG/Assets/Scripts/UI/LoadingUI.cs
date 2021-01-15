using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    int textCase;
    Text tipText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        textCase = Random.Range(0, 10);
        tipText = gameObject.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Text>();

        //gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;

        switch(textCase)
        {
            case 0:
                tipText.text = "골렘은 보기보다 강력합니다 조심하세요!";
                break;
            case 1:
                tipText.text = "샤먼의 파이어볼은 생각보다 느립니다.";
                break;
            case 2:
                tipText.text = "부장님은 가까이서 보면 더 귀엽습니다.";
                break;
            case 3:
                tipText.text = "사실, 산돌프의 마스코트는 샘이라는 사실을 알고 계신가요?";
                break;
            case 4:
                tipText.text = "혜림의 목소리는 만년설도 살살 녹인답니다.";
                break;
            case 5:
                tipText.text = "수현은 가장 잘생긴 디렉터입니다.";
                break;
            case 6:
                tipText.text = "산돌프는 귀요미 집합체라고 합니다!";
                break;
            case 7:
                tipText.text = "교수님은 신호를 보내는 일 말고 하시는 일이 무엇일까요?";
                break;
            case 8:
                tipText.text = "탈룰라는 룰라 탈퇴가 아닙니다.";
                break;
            case 9:
                tipText.text = "ㄷㄷㄷㅈ";
                break;
            case 10:
                tipText.text = "더이상 쓸말도 없다.. 아이디어 내놔..";
                break;
            default:
                break;
        }
    }
}
