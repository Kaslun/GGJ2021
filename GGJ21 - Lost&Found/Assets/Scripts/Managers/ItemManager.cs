using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] itemPool;

    public Item NewItem()
    {
        return itemPool[Random.Range(0, itemPool.Length)];
    }
}
