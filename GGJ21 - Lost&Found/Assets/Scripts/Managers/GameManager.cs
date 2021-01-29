using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;

    public static bool SameItem(Item lostItem, Item inStockItem)
    {
        if (lostItem == inStockItem)
            return true;
        else
            return false;
    }
}
