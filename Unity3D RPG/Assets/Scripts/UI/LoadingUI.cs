using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    int textCase;
    int imgCase;
    Text tipText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Sprite GetPath(int i) { return Resources.Load<Sprite>("LoadingImg" + i); }

    private void OnEnable()
    {
        textCase = Random.Range(0, 10);
        imgCase = Random.Range(0, 4);

        tipText = gameObject.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Text>();

        switch (imgCase)
        {
            case 0:
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = GetPath(0);
                break;
            case 1:
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = GetPath(1);
                break;
            case 2:
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = GetPath(2);
                break;
            case 3:
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = GetPath(3);
                break;
            case 4:
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = GetPath(4);
                break;
            default:
                break;
        }

        switch (textCase)
        {
            case 0:
                tipText.text = "스태미너를 다 쓰면 완전히 회복될 때까지 방어할 수 없어요."; 
                break;
            case 1:
                tipText.text = "혹시라도 화면이 답답하면 뷰 버튼을 눌러보세요.";
                break;
            case 2:
                tipText.text = "골렘은 보기보다 강력합니다 조심하세요!";
                break;
            case 3:
                tipText.text = "던전에 배치된 수상한 상자를 부셔본적 있나요?";
                break;
            case 4:
                tipText.text = "던전에 숨겨져있는 암상인을 찾아보세요.";
                break;
            case 5:
                tipText.text = "공격을 피하고 싶거나 빠른 이동을 원하면 구르기를 사용해보세요!";
                break;
            case 6:
                tipText.text = "가드 시 몸이 단단해져 뒤쪽에서 맞아도 안 아픕니다!";
                break;
            case 7:
                tipText.text = "트레이너에게 돈을 주고 캐릭터를 성장시킬 수 있습니다.";
                break;
            case 8:
                tipText.text = "강공격은 기본 공격과 다르게 경직 효과가 있습니다";
                break;
            case 9:
                tipText.text = "창고에서 가지고있는 전리품을 판매할 수 있습니다!";
                break;
            case 10:
                tipText.text = "재료를 모아 아이템을 강화하세요!";
                break;
            default:
                break;
        }
    }
}
