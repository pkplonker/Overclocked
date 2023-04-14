using Stuart;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private void Start()
    {
        GameController.Instance.OnGameOver += Lose;
        JobFactory.OnWin += Win;
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameOver -= Lose;
        JobFactory.OnWin -= Win;
    }


    private void Win()
    {
        winScreen.SetActive(true);
    }

    private void Lose(bool r)
    {
        loseScreen.SetActive(true);
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