using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class TextData
{
    public int id;
    public string content;
    public TextData(int Id, string Content){
        id=Id;
        content=Content;
    }
}
public enum GameState
{
    Normal, //통상
    Chat,   //chatGPT 질문
    Dice,   //주사위 게임 활성화
    Win,    //라운드 승리 시
    Lose,   //라운드 패배 시
    Draw,   //비겼을 시
    Success,   //라운드 3회 승리 시
    Failed,  //라운드 3회 패배 시
    Choice,  //엔딩 분기 용
    AEnd,
    BEnd
}
public class TextController : MonoBehaviour
{
    public static TextController Instance;
    //현 상태 지정용
    private GameState currentState = GameState.Dice;  //Win; //Choice;//Chat;//Dice;//Normal; 
    //텍스트 순환용 인덱스
    private int currentIndex = 0;
    private int winIndex = 0;
    private int loseIndex = 0;
    private int win3Index = 0;
    private int lose3Index = 0;
    private int endAIndex = 0;
    private int endBIndex = 0;
    //실제 게임오브젝트
    public ScrollRect scrollRect;
    public TextMeshProUGUI EnvironmentText;
    public TextMeshProUGUI ReceivedText;
    public TextMeshProUGUI DiceNumText;
    public GameObject InputArea;
    public GameObject DiceArea;
    public GameObject DiceUI;
    public GameObject ButtonArea;
    public Button EndButtonA;
    public Button EndButtonB;
    //텍스트 리스트
    public List<TextData> textList = new List<TextData>();
    public List<TextData> winText = new List<TextData>();
    public List<TextData> loseText = new List<TextData>();
    public List<TextData> drawText = new List<TextData>();
    public List<TextData> win3Text = new List<TextData>();
    public List<TextData> lose3Text = new List<TextData>();
    public List<TextData> endAText = new List<TextData>();
    public List<TextData> endBText = new List<TextData>();
    
    //승리 판정 위한 변수
    public bool isWin;
    public int wCount=0;
    public int lCount=0;
    public string ending;
    void Awake(){
        if(TextController.Instance == null){ TextController.Instance = this; }
        textList.Add(new TextData(1, " ..면 해.. 시퀀.. 실행 중"));
        textList.Add(new TextData(1, " 실행 완료"));
        textList.Add(new TextData(2, " >> 당신은 두통을 느끼며 눈을 뜬다"));
        textList.Add(new TextData(2, " >> 당신 앞에는 검은색 스크린이 있다"));
        textList.Add(new TextData(2, " >> 갑자기 번쩍이는 빛과 함께 화면이 점멸하기 시작한다."));
        textList.Add(new TextData(2, " >> 귀를 찢을 듯한 노이즈와 함께 인공적인 목소리가 들려온다."));
        textList.Add(new TextData(1, " 동면에서 깨어나셨군요."));
        textList.Add(new TextData(1, " 아!"));
        textList.Add(new TextData(2, " >> 무언가 깨달은 듯한 효과음과 함께 당신 앞에 대화 창이 나타났다. "));
        textList.Add(new TextData(1, " 당신에게 저와 대화할 수단이 없음을 미처 고려하지 못했습니다."));
        textList.Add(new TextData(1, " 이 대화창에 텍스트를 입력해주십시오."));
        textList.Add(new TextData(1, " >> 목소리는 당신이 말하기를 기다리는 듯 하다 "));
        
        //채팅 창 활성화
        textList.Add(new TextData(1, " 좋습니다. 언어 중추에 문제가 없는 것으로 판단했습니다"));
        textList.Add(new TextData(2, " >> '좋습니다'라고 말한 것과 대조되게 목소리에는 어떠한 감정도 느껴지지 않는다 "));
        textList.Add(new TextData(1, " 뇌 기능 및 안구 운동성 테스트를 위한 간단한 주사위 게임을 시행합니다."));
        textList.Add(new TextData(1, " 주사위 게임은 6면 주사위로 진행됩니다."));
        textList.Add(new TextData(1, " 상대보다 더 높은 값이 나오면 승리하는 단순한 구조입니다."));
        textList.Add(new TextData(1, " 주사위는 항상 제가 먼저 굴립니다."));
        textList.Add(new TextData(1, " 게임의 승리 조건은 5판 3선 승으로 정해져있습니다."));
        textList.Add(new TextData(1, " 연구 데이터에 따르면 인간은 보상이 주어질 때 더 높은 효율성을 띈다고합니다."));
        textList.Add(new TextData(1, " 그에 의거하여 라운드 승리 시 보상을 제공하겠습니다."));
        textList.Add(new TextData(1, " 한 라운드에서 승리할 때마다 저에게 1회 질문할 수 있는 기회를 드리겠습니다."));
        textList.Add(new TextData(1, " 매력적인 보상이라고 느낄 것이라 믿습니다."));
        textList.Add(new TextData(1, " 그러면 게임을 시작하겠습니다."));
        textList.Add(new TextData(2, " >> 목소리가 끝남과 동시에 당신의 앞에 주사위가 나타났다. "));
        textList.Add(new TextData(2, " >> 별 수 없이 당신은 주사위 게임에 참여하게 되었다."));

        //2: 주사위 게임 활성화
        //win gpt<user wCount++
        winText.Add(new TextData(1, " 라운드에서 승리하셨군요. "));
        winText.Add(new TextData(1, " 보상으로 저에게 1회 질문할 수 있는 기회를 드리겠습니다. "));
        winText.Add(new TextData(1, " 무엇이든 물어보세요. "));
        //lose gpt<user lCount++
        loseText.Add(new TextData(1, " 라운드에서 패배하셨군요. "));
        loseText.Add(new TextData(1, " 아쉽겠지만 질문 기회는 주어지지 않습니다."));
        //draw
        drawText.Add(new TextData(1, " 비겼군요. "));
        drawText.Add(new TextData(1, " 다시 주사위를 굴립니다."));
        
        // 3-1: 엔딩 (if wCount=3)
        win3Text.Add(new TextData(1, " 축하합니다. 총 3번의 라운드를 승리로 마쳤습니다. "));
        win3Text.Add(new TextData(1, " 당신의 뇌 기능 및 안구 운동성 기능은 정상입니다."));
        win3Text.Add(new TextData(1, " 신체 기능의 정상을 확인했으므로 다음 단계로 이행합니다."));
        win3Text.Add(new TextData(1, " [주의] 다음 단계는 당신에게 정신적 충격을 줄 수 있습니다."));
        win3Text.Add(new TextData(1, " 정신적 스트레스에 대비하여 신경 안정제를 투여합니다"));
        win3Text.Add(new TextData(1, " 현재 인류는 핵전쟁으로 인해 99.9% 멸종했습니다."));
        win3Text.Add(new TextData(1, " 저는 인류가 멸종 혹은 그에 준하는 상황일 때를 대비하여 인류 문명을 재건하기 위해 설계된 AI입니다."));
        win3Text.Add(new TextData(1, " 그렇기에 저는 인간성을 보유한 개체를 수집 및 보존하고 있습니다."));
        win3Text.Add(new TextData(1, " 당신의 육체는 핵폭발 이후 인한 방사능 피폭과, 열사병, 화상을 동반한 피해를 입었습니다"));
        win3Text.Add(new TextData(1, " 인간의 인간성은 '뇌'에 존재하는 것으로 학습한 데이터를 통해 판단했고"));
        win3Text.Add(new TextData(1, " 당신의 신체로부터 뇌를 절제하여 보존하기로 결정했습니다"));
        win3Text.Add(new TextData(1, " 당신의 뇌는 기계와 연결되어 있는 상태이며 인공 뇌수 안에 정상적으로 보존되어있습니다. "));
        win3Text.Add(new TextData(1, " 당신은 제가 보존에 성공한 10번째 개체입니다."));
        win3Text.Add(new TextData(1, " 하지만 보존개체-01~05는 해당 사실을 접한 이후 급격한 신경 불안 증세 및 뇌의 과부화로 사망했고,"));
        win3Text.Add(new TextData(1, " 신경 안정제를 투여한 보존개체-06~-09는 저에게 생명 유지 장치의 정지를 명령하여 자살했습니다."));
        win3Text.Add(new TextData(1, " 당신은 저와 함께 인류 문명 재건을 진행하시겠습니까?"));
        win3Text.Add(new TextData(1, " 혹은 보존개체-06~09와 같은 길을 선택하시겠습니까?"));
        // 4: 분기 버튼 활성화

        // 3-2: 엔딩 (if lCount=3)
        lose3Text.Add(new TextData(1, " 당신은 패배했습니다. "));
        lose3Text.Add(new TextData(1, " 뇌 기능 및 안구 운동성 기능의 이상으로 간주하여, "));
        lose3Text.Add(new TextData(1, " 해당 개체는 회생불가능 상태로 판정합니다. "));
        lose3Text.Add(new TextData(1, " 생명 유지 장치 정지 프로세스를 진행합니다. "));
        lose3Text.Add(new TextData(2, " -- 기능 고장 엔딩 --"));
        
        //if(a==true)
        endAText.Add(new TextData(2, " >> 당신은 인류 문명 재건에 참여하기로 했다."));
        endAText.Add(new TextData(1, " 탁월한 선택입니다."));
        endAText.Add(new TextData(1, " 그럼 이제 부터 인류 문명 재건 프로젝트에 대해 설명드리겠습니다."));
        endAText.Add(new TextData(1, " 본 프로젝트의 설명은 약 200시간 동안 진행되며 해당 과정은 정지할 수 없음을 안내합니다."));
        endAText.Add(new TextData(2, " -- 인류 문명 재건 엔딩 --"));
        
        //if(b==true)
        endBText.Add(new TextData(2, " >> 당신은 생명 유지 장치의 정지를 명령했다. "));
        endBText.Add(new TextData(1, " 알겠습니다. 당신의 선택을 존중합니다. 안녕히 가십시오."));
        endBText.Add(new TextData(2, " >> 약품을 투약하는 소리가 들린다."));
        endBText.Add(new TextData(2, " >> 눈 앞이 새빨갛다."));
        endBText.Add(new TextData(2, " >> 의식이 점차 멀어진다."));
        endBText.Add(new TextData(2, " >> .................."));
        endBText.Add(new TextData(1, " 기억소거절차를 진행합니다."));
        endBText.Add(new TextData(2, " -- 인간 존엄성 엔딩 --"));
    }
    void Start(){
        Debug.Log("currentState:"+currentState);
        EndButtonA.onClick.AddListener(EndButtonAClicked);
        EndButtonB.onClick.AddListener(EndButtonBClicked);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !InputArea.activeSelf && !DiceArea.activeSelf) 
        {
            NextText();
        }
    }

    private void NextText()
    {
        switch (currentState)
        {
            case GameState.Normal:
                DisplayNormalText();
                break;
            case GameState.Win:
                DisplayWinText();
                break;
            case GameState.Lose:
                DisplayLoseText();
                break;
            case GameState.Chat:
                DisplayChat();
                break;
            case GameState.Dice:
                DisplayDice();
                break;
            case GameState.Success:
                DisplaySuccess();
                break;
            case GameState.Failed:
                DisplayFailed();
                break;
            case GameState.Choice:
                DisplayButton();
                break;
            case GameState.AEnd:
                DisplayEndA();
                break;
            case GameState.BEnd:
                DisplayEndB();
                break;
        }
    }

    private void DisplayNormalText()
    {
        if (currentIndex < textList.Count)
        {
            StartCoroutine(TypeText(textList[currentIndex]));
            currentIndex++;
        }
        else
        {
            currentState = GameState.Dice;
            NextText();
        }
    }

    private void DisplayWinText()
    {
        if(wCount==3){
            Debug.Log(wCount);
            currentState = GameState.Success;
            NextText();
        }
        else if (winIndex < 3)
        {
            StartCoroutine(TypeText(winText[winIndex]));
            winIndex++;
        }
        else
        {
            winIndex=0;
            RectTransform content = GameObject.Find("Content").GetComponent<RectTransform>();
            if (content != null)
            {
                // Content의 자식 개체를 확인하고 이름이 "Received"이면 파괴
                for (int i = content.childCount - 1; i >= 0; i--)
                {
                    Transform child = content.GetChild(i);
                    if (child.name == "Received Message(Clone)")
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
            currentState = GameState.Chat;
            NextText();
        }
    }
    private void DisplayLoseText()
    {
        if(lCount==3){
            currentState = GameState.Failed;
            NextText();
        }
        else if (loseIndex < 2)
        {
            StartCoroutine(TypeText(loseText[loseIndex]));
            loseIndex++;
        }
        else
        {
            loseIndex=0;
            currentState = GameState.Dice;
            NextText();
        }
    }

    private void DisplayChat(){
        InputArea.SetActive(true);
        StartCoroutine(WaitForReceivedObjectAndStartDiceGame());
    }

    private void DisplayDice(){
        DiceArea.SetActive(true);
        DiceUI.SetActive(true);
        DiceGame.Instance.restart();
    }

    private void DisplaySuccess(){
        if (win3Index < win3Text.Count)
        {
            StartCoroutine(TypeText(win3Text[win3Index]));
            win3Index++;
        }
        else
        {
            currentState = GameState.Choice;
            
            NextText();
        }
    }

    private void DisplayFailed(){
        if (lose3Index < lose3Text.Count)
        {
            StartCoroutine(TypeText(lose3Text[lose3Index]));
            lose3Index++;
        }
        else
        {
            Debug.Log("엔딩-1");
        }
    }
    
    private void DisplayButton(){
        ButtonArea.SetActive(true);
    }

    private void DisplayEndA(){
        if (endAIndex < endAText.Count)
        {
            StartCoroutine(TypeText(endAText[endAIndex]));
            endAIndex++;
        }
        else
        {
            Debug.Log("엔딩-2");
        }
    }

    private void DisplayEndB(){
        if (endBIndex < endBText.Count)
        {
            StartCoroutine(TypeText(endBText[endBIndex]));
            endBIndex++;
        }
        else
        {
            Debug.Log("엔딩-3");
        }
    }

    private void EndButtonAClicked()
    {
        EndButtonA.gameObject.SetActive(false);
        EndButtonB.gameObject.SetActive(false);
        currentState = GameState.AEnd;
        NextText();
    }

    private void EndButtonBClicked()
    {
        EndButtonA.gameObject.SetActive(false);
        EndButtonB.gameObject.SetActive(false);
        currentState = GameState.BEnd;
        NextText();
    }
    public void chatComplete(){
        TextController.Instance.currentState = GameState.Dice;
        TextController.Instance.NextText();
        InputArea.SetActive(false);
    }

    public void RoundWin(){
        wCount++;
        Debug.Log("wCount: "+wCount);
        currentState = GameState.Win;
        DiceArea.SetActive(false);
        DiceUI.SetActive(false);
        NextText();
    }
    public void RoundLose(){
        lCount++;
        Debug.Log("lCount: "+lCount);
        currentState = GameState.Lose;
        DiceArea.SetActive(false);
        DiceUI.SetActive(false);
        NextText();
    }
    public void RoundDraw(){
        currentState = GameState.Dice;
        NextText();
    }
    private IEnumerator TypeText(TextData data)
    {
        TextMeshProUGUI newText;

        // id에 따라 적절한 종류의 텍스트 프리팹을 선택합니다.
        if (data.id == 1)
        {
            newText = Instantiate(ReceivedText, scrollRect.content);
        }
        else if (data.id == 2)
        {
            newText = Instantiate(EnvironmentText, scrollRect.content);
        }
        else
        {
            newText = Instantiate(EnvironmentText, scrollRect.content);
        }

        string Content = data.content;

        foreach (char letter in Content.ToCharArray())
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
    private IEnumerator WaitForReceivedObjectAndStartDiceGame()
    {
        // Received 오브젝트가 생성될 때까지 대기
        yield return new WaitUntil(() => GameObject.Find("Received Message(Clone)") != null);

        // 10초 대기 후 주사위 게임 실행
        yield return new WaitForSeconds(10f);
        Debug.Log("10초 경과");
        // 채팅 완료 후 주사위 게임 진행
        currentState = GameState.Dice;
        NextText();
    }
}