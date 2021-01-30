using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public Image charAvatar;
    public Image itemAvatar;
    public Item charItem;
    public Item inStockItem;

    public Character currentChar;
    public Character[] characters;
    public ItemManager itemManager;
    public ConvoManager convoManager;

    private void Awake()
    {
        EventManager.StartListening("Next", Next);
        ChangeCharacter();
    }

    private void Next()
    {
        //SpawnCharacter();
        ChangeCharacter();
    }

    public void ChangeCharacter()
    {
        int newChar = Random.Range(0, characters.Length);

        print("Changing");
        if (characters[newChar] == currentChar) ChangeCharacter();

        currentChar = characters[newChar];

        UpdateData(currentChar);
    }

    private void UpdateData(Character character)
    {
        convoManager.character = character;
        charAvatar.sprite = character.avatar;

        character.lostObject = itemManager.NewItem();
        charItem = character.lostObject;

        inStockItem = itemManager.NewItem();
        itemAvatar.sprite = inStockItem.avatar;
    }
}
