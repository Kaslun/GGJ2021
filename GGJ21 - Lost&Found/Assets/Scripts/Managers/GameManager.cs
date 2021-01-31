using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;

    //Timelimit written i num of seconds
    public int timeLimit;
    public Text scoreText;
    public Text endScoreText;
    public Text timerText;

    public MenuManager menuManager;

    private int numOfCharacters = 0;

    private int resetTimeLimit;

    public void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        TriggerNext();
        EventManager.StartListening("Next", Next);
        resetTimeLimit = timeLimit;
    }

    public void TriggerNext()
    {
        EventManager.TriggerEvent("Next");
    }

    public void StartRound()
    {
        timeLimit = resetTimeLimit;
        StartCoroutine(StartTimer());
    }

    public void Next()
    {
        numOfCharacters++;
    }

    public void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private IEnumerator StartTimer()
    {
        if(timeLimit > 0)
        {
            timeLimit--;
            timerText.text = timeLimit.ToString();
            yield return new WaitForSeconds(1);
            StartCoroutine(StartTimer());
            yield break;
        }

        else
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
        if(score <= numOfCharacters/3)
        {
            endScoreText.text = "You got through " + numOfCharacters + " and ended up with a score of " + score + " points. Want to try again?";
        }
        else
            endScoreText.text = "You got through " + numOfCharacters + " and ended up with a score of " + score + " points! Good job!";

        numOfCharacters = 0;
        score = 0;
    }
}
