using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    public void Start()
    {
        EventManager.TriggerEvent("Next");
    }

    public void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public static bool SameItem(Item lostItem, Item inStockItem)
    {
        if (lostItem == inStockItem)
            return true;
        else
            return false;
    }
}
