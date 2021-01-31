using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] itemPool;
    public string[] colorPool;


    public Item NewItem()
    {
        Item item = itemPool[Random.Range(0, itemPool.Length)];
        string color = colorPool[Random.Range(0, colorPool.Length)];

        item.color = color;
        return item;
    }
}
