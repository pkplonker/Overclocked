using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Stuart;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public Canvas PauseUI;

    public void Awake()
    {
        Play();
    }

    public void Start()
    {
        GameController.Instance.OnPauseChanged += PausedChanged;
    }

    public void PausedChanged(bool value)
    {
        Debug.Log(value);

        if (value)
        {
            Pause();
        }
        else
        {
            Play();
        }
    }

    public void Play()
    {
        PauseUI.enabled = false;

    }

    public void Pause()
    {
        PauseUI.enabled = true;
    }

    public void QuitButton()
    {
        Debug.Log("Game has Exited");
        Application.Quit();
    }

    public void MainMenuButtonLevel()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Continue()
    {
        
    }
}