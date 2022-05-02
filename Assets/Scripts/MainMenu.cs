using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitPressed()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }

    public void ReplayPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuPressed()
    {
        SceneManager.LoadScene(0);
    }
}
