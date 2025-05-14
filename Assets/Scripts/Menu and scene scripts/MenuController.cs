using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Planting");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLocker()
    {
        SceneManager.LoadScene("Locker");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}