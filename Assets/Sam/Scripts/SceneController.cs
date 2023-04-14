using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void level3()
    {
        SceneManager.LoadScene("Level_3");
    }

    public void level4()
    {
        SceneManager.LoadScene("Level_4");
    }

    public void level5()
    {
        SceneManager.LoadScene("Level_5");
    }

    public void level6()
    {
        SceneManager.LoadScene("Level_6");
    }

    public void EndGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}