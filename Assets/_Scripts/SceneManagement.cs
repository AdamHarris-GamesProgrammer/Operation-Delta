using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void RetryLevel()
    {
        Debug.Log("Retry Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Return to main menu");
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
