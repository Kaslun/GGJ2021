using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public Text endScoreText;

    public MenuManager menuManager;

    private int numOfCharacters = 0;

    public void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        TriggerNext();
        EventManager.StartListening("Next", Next);
    }

    public void TriggerNext()
    {
        EventManager.TriggerEvent("Next");
    }

    public void Next()
    {
        numOfCharacters++;
    }

    public void Update()
    {
        scoreText.text = "Score: " + score.ToString();

        if(numOfCharacters > 20)
        {
            GameOver();
        }
    }

    public static bool SameItem(Item lostItem, Item inStockItem)
    {
        if (lostItem == inStockItem)
            return true;
        else
            return false;
    }

    public void GameOver()
    {
        menuManager.SwitchMenu(3);
        if(score <= 0)
        {
            endScoreText.text = "You got " + score + " points. Want to try again?";
        }
        else
            endScoreText.text = "You got " + score + " points! Good job!";

        numOfCharacters = 0;
        score = 0;
    }
}
