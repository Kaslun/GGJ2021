using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    public void Update()
    {
        scoreText.text = score.ToString();
    }

    public static bool SameItem(Item lostItem, Item inStockItem)
    {
        if (lostItem == inStockItem)
            return true;
        else
            return false;
    }
}
