using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this line to use SceneManager

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credits;

    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }

    public void StartGame()
    {
        // Load the next scene (you need to set up your scenes in the build settings)
        SceneManager.LoadScene("GameFinalScene");
    }

    public void OnApplicationQuit()
    {
        // Quit the application
        Application.Quit();
    }

    public void ShowCredits()
    {
        // Toggle between main menu and credits
        mainMenu.SetActive(!mainMenu.activeSelf);
        credits.SetActive(!credits.activeSelf);
    }
}
