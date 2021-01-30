using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menus;

    [SerializeField]
    int currentMenu = 0;
    int prevMenu;

    private void Awake()
    {
        EventManager.StartListening("GoBack", GoBack);
        SwitchMenu(0);
    }

    public void SwitchMenu(int newMenu)
    {
        prevMenu = currentMenu;
        currentMenu = newMenu;

        foreach(GameObject o in menus)
        {
            o.SetActive(false);
        }

        menus[currentMenu].SetActive(true);
    }

    public void GoBack()
    {
        SwitchMenu(prevMenu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
