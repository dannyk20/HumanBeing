using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceNumText : MonoBehaviour
{
    public static DiceNumText Instance;
    public TextMeshProUGUI text;
    public static int diceNumber;
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        text.text=diceNumber.ToString();
    }
    public int getDiceNum(){
        return diceNumber;
    }
}
