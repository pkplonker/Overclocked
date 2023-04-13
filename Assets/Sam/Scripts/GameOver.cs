using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void NextLevel()
    {

    }
    
    public void RetryLevel()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
