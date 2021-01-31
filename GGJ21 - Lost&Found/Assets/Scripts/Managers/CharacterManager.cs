using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
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
        go.transform.position = characterPoint.position;
        inputManager.avatar = go.transform;
    }

    private void UpdateData(Character character)
    {
        character.lostObject = itemManager.NewItem();
        charItem = character.lostObject;
        inStockItem = itemManager.NewItem();
    }

    private void UpdateText()
    {
        outputTxt.text = "Hi! I'm " + currentChar.name + " and I'm looking for a " + charItem.color + " " + charItem.name + ".";
        itemTxt.text = inStockItem.color + " " + inStockItem.name;
    }
}
