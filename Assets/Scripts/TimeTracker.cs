using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTracker : MonoBehaviour
{
    public float time;
    public static TimeTracker Instance;
    public Text TimeText;
    public Text BestTimeText;
    float seconds, minutes, hs_seconds, hs_minutes, besttime;
    public bool start;


    void Awake()
    {
        Instance = this;
        start = true;
        time = 0;
        //FOR RESET BEST TIME
        //PlayerPrefs.DeleteAll(); 

        if (PlayerPrefs.HasKey("BestTime"))
        {
            ShowBestTime();
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        TimeCalcu();
    }

    private void TimeCalcu()
    {
        if (start)
        {
            time += Time.deltaTime;
            seconds = (int)(time % 60);
            minutes = (int)(time / 60);
            TimeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    public void UpdateBestTime(float time)
    {
        //IF BEST TIME IS SET
        if (PlayerPrefs.HasKey("BestTime"))
        {
            if (PlayerPrefs.GetFloat("BestTime", time) >= time)
            {
                PlayerPrefs.SetFloat("BestTime", time);
                ShowBestTime();
            }
        }
        //IF BEST TIME NOT SET
        else
        {
            PlayerPrefs.SetFloat("BestTime", time);
            ShowBestTime();
        }
    }

    public void ShowBestTime()
    {
        besttime = PlayerPrefs.GetFloat("BestTime");
        hs_seconds = (int)(besttime % 60);
        hs_minutes = (int)(besttime / 60);
        BestTimeText.text = hs_minutes.ToString("00") + ":" + hs_seconds.ToString("00");
    }
}
