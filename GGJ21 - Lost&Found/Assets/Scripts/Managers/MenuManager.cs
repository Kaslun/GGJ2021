using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menus;

    [SerializeField]
    int currentMenu = 0;

    private void Awake()
    {
        SwitchMenu(0);
    }

    public void SwitchMenu(int newMenu)
    {
        foreach(GameObject o in menus)
        {
            o.SetActive(false);
        }

        menus[newMenu].SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
