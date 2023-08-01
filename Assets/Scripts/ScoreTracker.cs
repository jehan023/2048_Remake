using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    private int score;
    public static ScoreTracker Instance;
    public Text ScoreText;
    public Text HighScoreText;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            ScoreText.text = score.ToString();
            if (SceneManager.GetActiveScene().name == "x5GameScene")
            {
                if (PlayerPrefs.GetInt("x5HighScore") < score)
                {
                    PlayerPrefs.SetInt("x5HighScore", score);
                    HighScoreText.text = score.ToString();
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("x4HighScore") < score)
                {
                    PlayerPrefs.SetInt("x4HighScore", score);
                    HighScoreText.text = score.ToString();
                }
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        //for reset highscore
        //PlayerPrefs.DeleteAll(); 
        if (SceneManager.GetActiveScene().name == "x5GameScene")
        {
            if (!PlayerPrefs.HasKey("x5HighScore"))
                PlayerPrefs.SetInt("x5HighScore", 0);

            ScoreText.text = "0";
            HighScoreText.text = PlayerPrefs.GetInt("x5HighScore").ToString();
        }
        else
        {
            if (!PlayerPrefs.HasKey("x4HighScore"))
                PlayerPrefs.SetInt("x4HighScore", 0);

            ScoreText.text = "0";
            HighScoreText.text = PlayerPrefs.GetInt("x4HighScore").ToString();
        }
            

    }
}
