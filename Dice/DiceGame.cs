using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DiceGame : MonoBehaviour
{
    public static DiceGame Instance;
    public string GPTAnswer;
    string numberString;
    public int userDice;
    public int GPTDice;
    public TextMeshProUGUI Dice_user;
    public TextMeshProUGUI Dice_GPT;
    public GameObject DiceWin;
    public GameObject DiceLose;
    public GameObject DiceDraw;
    public string result;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
        /*GPTAnswer=ChatGPT.Instance.getGptDice();
        numberString=Regex.Match(GPTAnswer, @"\d+").Value;
        if (int.TryParse(numberString, out int Dresult))
        {
            // 정상적으로 변환된 경우
            GPTDice=Dresult;
        }
        else
        {
            // 변환 실패한 경우
            Console.WriteLine("Failed to extract a valid number.");
        }
        */
        GPTDice = 6;
        Dice_GPT.text="GPT의 주사위값: "+GPTDice.ToString();
        
    }
    public void resetGame()
    {
        Debug.Log("reset");
        // UI 초기화
        DiceWin.SetActive(false);
        DiceLose.SetActive(false);
        DiceDraw.SetActive(false);
        
    }
    
    public void restart(){
        while (userDice == 0)
        {
            resetGame();
        }
        StartCoroutine(WaitForNonZeroUserDice());
    }

    // 주사위 비교 및 결과 반환 함수
    public string CompareDice(int userValue, int gptValue)
    {
        if (userValue > gptValue)
        {
            DiceWin.SetActive(true);
            StartCoroutine(WaitForThreeSecondsW());
            return "Win";
            
        }
        else if (userValue < gptValue)
        {
            DiceLose.SetActive(true);
            StartCoroutine(WaitForThreeSecondsL());
            return "Lose";
        }
        else
        {   
            DiceDraw.SetActive(true);
            StartCoroutine(WaitForThreeSecondsD());
            return "Draw";
        }
    }
    private IEnumerator WaitForNonZeroUserDice()
    {
        // userDice 값이 0이 아닐 때까지 대기
        while (userDice == 0)
        {
            userDice = DiceCheck.Instance.getDiceNum();
            yield return null; // 다음 프레임까지 대기
        }
        userDice = DiceCheck.Instance.getDiceNum(); 
        Debug.Log(userDice);
        
        Dice_user.text="플레이어의 주사위값: "+userDice.ToString();
        // 결과 비교 및 출력
        if(userDice!=0 && GPTDice!=0){
            string result = CompareDice(userDice, GPTDice);
            Debug.Log(result);
        }
    }
    private IEnumerator WaitForThreeSecondsW()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("3초가 경과하였습니다.");
        TextController.Instance.RoundWin();
    }
    private IEnumerator WaitForThreeSecondsL()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("3초가 경과하였습니다.");
        TextController.Instance.RoundLose();
    }
    private IEnumerator WaitForThreeSecondsD()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("3초가 경과하였습니다.");
        StartCoroutine(WaitForNonZeroUserDice());
    }
}