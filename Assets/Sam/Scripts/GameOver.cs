using System;
using System.Collections;
using System.Collections.Generic;
using Stuart;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private void Start()
    {
        GameController.Instance.OnGameOver += (_) => loseScreen.SetActive(true);
        JobFactory.OnWin += () => winScreen.SetActive(true);

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
     
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
