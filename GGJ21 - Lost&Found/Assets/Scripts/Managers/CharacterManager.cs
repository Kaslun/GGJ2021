﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    //public Image charAvatar;
    public Image itemAvatar;
    public Item charItem;
    public Item inStockItem;

    public Text outputTxt;
    public Text itemTxt;

    public GameObject characterPrefab;
    public GameObject characterPanel;
    public Transform characterPoint;

    public Character currentChar;
    public Character[] characters;
    public ItemManager itemManager;
    public InputManager inputManager;

    private void Awake()
    {
        EventManager.StartListening("Next", Next);
    }

    private void Next()
    {
        ChangeCharacter();
    }

    public void ChangeCharacter()
    {
        int newChar = Random.Range(0, characters.Length);

        print("Changing");
        if (characters[newChar] == currentChar) ChangeCharacter();

        currentChar = characters[newChar];

        UpdateData(currentChar);
        UpdateText();
        SpawnCharacter();
    }

    public void SpawnCharacter()
    {
        Destroy(inputManager.avatar.gameObject);
        GameObject go = Instantiate(characterPrefab, characterPanel.transform);
        go.name = currentChar.name;
        go.transform.position = new Vector2(characterPoint.position.x, characterPoint.position.y - 300);
        inputManager.avatar = go.transform;
    }

    private void UpdateData(Character character)
    {
        //charAvatar.sprite = character.avatar;

        character.lostObject = itemManager.NewItem();
        charItem = character.lostObject;

        inStockItem = itemManager.NewItem();
        itemAvatar.sprite = inStockItem.avatar;
    }

    private void UpdateText()
    {
        outputTxt.text = "Hi! I'm " + currentChar.name + " and I'm looking for a " + charItem.color + " " + charItem.name + ".";
        itemTxt.text = inStockItem.color + " " + inStockItem.name;
    }
}
