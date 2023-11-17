using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TextValue : MonoBehaviour
{
    public ScrollRect scrollRect;
    private int currentIndex = 0;
    public TextMeshProUGUI EnvironmentText;
    public TextMeshProUGUI ReceivedText;
    private Coroutine typingCoroutine;
    private List<TextData> textList = new List<TextData>{

        /*new TextData(id=1, " ..면 해.. 시퀀.. 실행 중");
        new TextData(id=1, " 실행 완료");
        new TextData(id=2, " >> 당신은 두통을 느끼며 눈을 뜬다");
        new TextData(id=2, " >> 당신 앞에는 검은색 스크린이 있다");
        new TextData(id=2, " >> 갑자기 번쩍이는 빛과 함께 화면이 점멸하기 시작한다.");
        new TextData(id=2, " >> 귀를 찢을 듯한 노이즈와 함께 인공적인 목소리가 들려온다.");

        new TextData(id=1, " 동면에서 깨어나셨군요");
        new TextData(id=1, " 아!");
        new TextData(id=2, " >> 무언가 깨달은 듯한 효과음과 함께 당신 앞에 대화 창이 나타났다. ");
        new TextData(id=1, " 당신에게 저와 대화할 수단이 없음을 미처 고려하지 못했습니다.");
        new TextData(id=1, " 이 대화창에 텍스트를 입력해주십시오.");
        new TextData(id=1, " >> 목소리는 당신이 말하기를 기다리는 듯 하다 ");
        new TextData(id=1, " 좋습니다. 언어 중추에 문제가 없는 것으로 판단했습니다");
        new TextData(id=2, " >> '좋습니다'라고 말한 것과 대조되게 목소리에는 어떠한 감정도 느껴지지 않는다 ");
        new TextData(id=1, " 뇌 기능 및 안구 운동성 테스트를 위한 간단한 주사위 게임을 시행합니다.");
        new TextData(id=1, " 주사위 게임은 6면 주사위로 진행됩니다.");
        new TextData(id=1, " 상대보다 더 높은 값이 나오면 승리하는 단순한 구조입니다.");
        new TextData(id=1, " 주사위는 항상 제가 먼저 굴립니다.");
        new TextData(id=1, " 화면 우측 상단에 보이는 하트 아이콘이 승리 횟수 입니다.");
        new TextData(id=1, " 게임의 승리 조건은 5판 3선 승으로 정해져있습니다.");
        new TextData(id=1, " 연구 데이터에 따르면 인간은 보상이 주어질 때 더 높은 효율성을 띈다고합니다.");
        new TextData(id=1, " 그에 의거하여 라운드 승리 시 보상을 제공하겠습니다.");
        new TextData(id=1, " 한 라운드에서 승리할 때마다 저에게 1회 질문할 수 있는 기회를 드리겠습니다.");
        new TextData(id=1, " 매력적인 보상이라고 느낄 것이라 믿습니다.");
        new TextData(id=1, " 그러면 게임을 시작하겠습니다.");
        new TextData(id=2, " >> 목소리가 끝남과 동시에 당신의 앞에 주사위가 나타났다. ");
        new TextData(id=2, " >> 별 수 없이 당신은 주사위 게임에 참여하게 되었다");

        new TextData(id=1, " 1라운드에서 승리하셨군요 ");
        new TextData(id=1, " 보상으로 저에게 1회 질문할 수 있는 기회를 드리겠습니다. ");
        new TextData(id=2, " 무엇이든 물어보세요 ");

        new TextData(id=1, " 1라운드에서 패배하셨군요 ");
        new TextData(id=1, " 아쉽겠지만 질문 기회는 주어지지 않습니다");

        
        new TextData(id=1, " 축하합니다. 총 3번의 라운드를 승리로 마쳤습니다. ");
        new TextData(id=1, " 당신의 뇌 기능 및 안구 운동성 기능은 정상입니다.");
        new TextData(id=1, " 신체 기능의 정상을 확인했으므로 다음 단계로 이행합니다.");
        new TextData(id=1, " 다음 단계는 당신에게 정신적 충격을 줄 수 있습니다.");
        new TextData(id=1, " 정신적 스트레스에 대비하여 신경 안정제를 투여합니다");
        new TextData(id=1, " 현재 인류는 핵전쟁으로 인해 99.9% 멸종했습니다.");
        new TextData(id=1, " 저는 인류가 멸종 혹은 그에 준하는 상황일 때를 대비하여 인류 문명을 재건하기 위해 설계된 AI입니다.");
        new TextData(id=1, " 그렇기에 저는 인간성을 보유한 개체를 수집 및 보존하고 있습니다.");
        new TextData(id=1, " 당신의 육체는 핵폭발 이후 인한 방사능 피폭과, 열사병, 화상을 동반한 피해를 입었습니다");
        new TextData(id=1, " 인간의 인간성은 '뇌'에 존재하는 것으로 학습한 데이터를 통해 판단했고");
        new TextData(id=1, " 당신의 신체로부터 뇌를 절제하여 보존하기로 결정했습니다");
        new TextData(id=1, " 당신의 뇌는 기계와 연결되어 있는 상태이며 인공 뇌수 안에 정상적으로 보존되어있습니다. ");
        new TextData(id=1, " 당신은 제가 보존에 성공한 10번째 개체입니다.");
        new TextData(id=1, " 하지만 보존개체-01~05는 해당 사실을 접한 이후 급격한 신경 불안 증세 및 뇌의 과부화로 사망했고");
        new TextData(id=1, " 신경 안정제를 투여한 보존개체-06~-09는 저에게 생명 유지 장치의 정지를 명령하여 자살했습니다.");
        new TextData(id=1, " 당신은 저와 함께 인류 문명 재건을 진행하시겠습니까?");
        new TextData(id=1, " 혹은 보존개체-06~09와 같은 길을 선택하시겠습니까?");
        new TextData(id=2, " >> 당신은 인류 문명 재건에 참여하기로 했다.");
        new TextData(id=1, " 탁월한 선택입니다.");
        new TextData(id=1, " 그럼 이제 부터 인류 문명 재건 프로젝트에 대해 설명드리겠습니다.");
        new TextData(id=1, " 본 프로젝트의 설명은 약 200시간 동안 진행되며 해당 과정은 정지할 수 없음을 안내합니다.");
        new TextData(id=2, " -- 인류 문명 재건 엔딩 --");

        new TextData(id=2, " >> 당신은 생명 유지 장치의 정지를 명령했다. ");
        new TextData(id=1, " 알겠습니다. 당신의 선택을 존중합니다. 안녕히 가십시오.");
        new TextData(id=1, " >> 약품을 투약하는 소리가 들린다.");
        new TextData(id=1, " >> 눈 앞이 새빨갛다.");
        new TextData(id=1, " >> 의식이 점차 멀어진다.");
        new TextData(id=2, " >> ..................");
        new TextData(id=1, " 기억소거절차를 진행합니다.");
        new TextData(id=2, " -- 인간 존엄성 엔딩 --");*/

    };
    

    private void Start()
    {
        DisplayNextText();
    }

    void Update()
    {
        // 스페이스바를 누르면 다음 텍스트를 표시합니다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextText();
        }
    }

    private void DisplayNextText()
    {
        // 리스트의 다음 텍스트를 표시하고 인덱스를 증가시킵니다.
        if (currentIndex < textList.Count)
        {
            //StartCoroutine(TypeText(textList[currentIndex]));
            currentIndex++;
        }
        else
        {
            Debug.Log("더 이상 텍스트가 없습니다.");
        }
    }

    private IEnumerator TypeText(string content)
    {
        // 한 글자씩 나타내는 코루틴
        TextMeshProUGUI newText = Instantiate(EnvironmentText, scrollRect.content);
        newText.text = "";

        foreach (char letter in content.ToCharArray())
        {
            newText.text += letter;

            // Content의 크기를 자동으로 조절합니다.
            RectTransform contentRect = scrollRect.content.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentRect.sizeDelta.y + newText.preferredHeight);

            // 스크롤을 맨 아래로 이동시킵니다.
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;

            yield return new WaitForSeconds(0.05f); // 한 글자씩 나타나는 시간 간격 조절
        }
    }
}
