using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConvoManager : MonoBehaviour
{
    public Character character;

    public TextMeshProUGUI outputTxt;
    public void AskQuestion(int questionNumber)
    {
        switch (questionNumber)
        {
            case 0: 
                outputTxt.text = character.name;
                break;
            case 1:
                outputTxt.text = character.lostObject.color;
                break;
            case 2:
                outputTxt.text = character.lostObject.name;
                break;
        }
    }
}
