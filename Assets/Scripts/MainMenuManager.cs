using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void TimeGameButtonHandler()
    {
        SceneManager.LoadScene("TimeGameScene");
    }

    public void Game4x4ButtonHandler()
    {
        SceneManager.LoadScene("x4GameScene");
    }

    public void Game5x5ButtonHandler()
    {
        SceneManager.LoadScene("x5GameScene");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void ResetButton()
    {
        PlayerPrefs.DeleteKey("x5HighScore");
        PlayerPrefs.DeleteKey("x4HighScore");
        PlayerPrefs.DeleteKey("BestTime");
    }
}
