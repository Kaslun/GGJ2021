using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameManager gameManager;
    private CharacterManager charManager;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        charManager = FindObjectOfType<CharacterManager>();
    }

    public void Accept()
    {
        if (GameManager.SameItem(charManager.charItem, charManager.inStockItem))
            gameManager.score++;
        else
            gameManager.score--;
        charManager.ChangeCharacter();
    }

    public void Deny()
    {
        if (!GameManager.SameItem(charManager.charItem, charManager.inStockItem))
            gameManager.score++;
        else
            gameManager.score--;
        charManager.ChangeCharacter();
    }
}
