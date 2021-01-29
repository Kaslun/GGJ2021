using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    private RawImage avatar;
    public Item charItem;
    public Item inStockItem;

    public Character currentChar;
    public Character[] characters;
    public ItemManager itemManager;
    public ConvoManager convoManager;

    private void Awake()
    {
        ChangeCharacter();
    }

    public void ChangeCharacter()
    {
        int newChar = Random.Range(0, characters.Length);
        print("Changing");
        if (characters[newChar] == currentChar) ChangeCharacter();

        currentChar = characters[newChar];

        UpdateCharacter(currentChar);
    }

    private void UpdateCharacter(Character character)
    {
        convoManager.character = character;
        avatar = character.avatar;
        character.lostObject = itemManager.NewItem();
        charItem = character.lostObject;
        inStockItem = itemManager.NewItem();
    }
}
