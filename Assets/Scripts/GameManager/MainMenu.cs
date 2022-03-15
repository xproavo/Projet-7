using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject commands;
    public GameObject menu;

    //public GameObject settingsWindow;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void CommandsButton()
    {
        commands.SetActive(true);
        menu.SetActive(false);
    }

    public void CloseCommandsWindow()
    {
        commands.SetActive(false);
        menu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}