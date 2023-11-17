using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceCheck : MonoBehaviour
{
    public static DiceCheck Instance;
    public TextMeshProUGUI DiceNumText;
    public int diceNumber;
    Vector3 diceVelocity;

    void Awake()
    {
        Instance = this;
    }
    void FixedUpdate () {
        diceVelocity = DiceRoll.diceVelocity;
    }

    void OnTriggerStay(Collider col){
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f){
            switch (col.gameObject.name){
                case "Side1":
                    diceNumber=6;
                    diceNumber.ToString();
                    break;
                case "Side2":
                    diceNumber=5;
                    diceNumber.ToString();
                    break;
                case "Side3":
                    diceNumber=4;
                    diceNumber.ToString();
                    break;
                case "Side4":
                    diceNumber=3;
                    diceNumber.ToString();
                    break;
                case "Side5":
                    diceNumber=2;
                    diceNumber.ToString();
                    break;
                case "Side6":
                    diceNumber=1;
                    diceNumber.ToString();
                    break;
            }
        }
    }
    public int getDiceNum(){
        return diceNumber;
    }
}
