using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Intro : MonoBehaviour
{
    Button Go;
    Button Left;
    Button Right;
    Text Contents;

    public GameObject IntroCanvas;

    int curPage = 0;

    private void Update()
    {
        switch (curPage)
        {
            case 0:
                Left.gameObject.SetActive(false);

                Contents.text = "흐아암~..\r\n " +
                "침대에서 일어난 당신은 기지개를 피며 일어난다.\r\n" +
                "무리하게 대출까지 받아가면 구입한\r\n" +
                "집에대한 애정이 매우 강했다.\r\n" +
                "흐뭇한 미소로 집을 감상하는 것도 잠시,\r\n" +
                "당신은 순식간에 나갈 채비를 마쳤다.";

                break;
            case 1:
                Left.gameObject.SetActive(true);

                Contents.text = "출발해볼까?\r\n" +
                "곧이어 마을 바깥에 있는 검술 훈련장으로 발걸음을 옮긴다.\r\n\r\n" +
                "위요오오오오옹! 애용애용애용애용!\r\n\r\n" +
                "검술을 익히던 중 마을에서 비상 알림이 들려 급하게 돌아간다.\r\n" +
                "마을에 거의 다다르자 당신의 집 앞에 많은 사람들이 모여있는것을 볼 수 있었다.";

                break;
            case 2:
                Contents.text = "....\r\n" +
                "불안한 마음도 잠시, 당신은 처참한 광경에 할 말을 잃는다.\r\n" +
                "이.. 이게.. 무슨...\r\n" +
                "당신의 집은 이미 산산히 부서져있었고\r\n" +
                "말을 잇지 못하는 당신에게 촌장 세이크가 자초지종을 설명한다.";

                break;
            case 3:
                Contents.text = "던전 몬스터가 마을을 습격해 집을 다 부숴놓았네..\r\n" +
                "아직 대출금이 100000gold 나 남은 당신은\r\n" +
                "참을 수 없는 분노를 느끼며 던전으로 향한다\r\n" +
                "하지만 던전의 몬스터는 매우 강했고 당신은 기절해 중앙 광장에서 눈을 뜬다.\r\n" +
                "강해지고싶다면 나를 따라오게나.\r\n" +
                "촌장 세이크가 말했다.";

                break;
            case 4:
                Right.gameObject.SetActive(true);

                Contents.text = "트레이너에게 훈련을 받으면 강해질 수 있지.\r\n" +
                "무기 강화는 대장장이에게 맡기게나.\r\n" +
                "마을 창고를 빌려줄테니 던전에서 먿은 아이템과 장비를 보관하게나\r\n" +
                "보험사에게 보험을 들으면 던전에서 죽어도 미련이 없을게야\r\n" +
                "촌장 세이크는 마을의 주민들을 당신에게 소개시켜주었다.";

                break;
            case 5:
                Right.gameObject.SetActive(false);
                Go.gameObject.SetActive(true);

                Contents.text = "당신은 밀린 대출금을 갚기 위하여, 그리고 복수를 위하여 오늘도 집을 나선다.\r\n\r\n" +
                "(입장 버튼을 눌러 마을로 입장하시오)";

                break;
            default:
                break;
        }
    }
    private void OnEnable()
    {
        Go = this.gameObject.transform.GetChild(3).GetComponent<Button>();
        Left = this.gameObject.transform.GetChild(2).GetComponent<Button>();
        Right = this.gameObject.transform.GetChild(1).GetComponent<Button>();
        Contents = this.gameObject.transform.GetChild(0).GetComponent<Text>();
    }

    public void ClickRight()
    {
        curPage++;
    }
    public void ClickLeft()
    {
        curPage--;
    }
    public void SkipIntro()
    {
        IntroCanvas.SetActive(false);
    }
}
