using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{

    #region Singleton
    public static GameTimer Instance;

    void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    #endregion

    public double currentTime;

    private TMP_Text currentTimer;
    private bool timerActive = true;

    void Start()
    {
        currentTimer = GetComponent<TMP_Text>();

        if (PlayerPrefs.HasKey("Time"))
            currentTime = PlayerPrefs.GetFloat("Time");
        else
            PlayerPrefs.SetFloat("Time", 0f);

        currentTimer.text = GetTime();
        timerActive = true;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
            currentTimer.text = GetTime();
        }
    }

    string GetTime()
    {
        float minutes = Mathf.FloorToInt((float)(currentTime / 60d));
        float seconds = Mathf.FloorToInt((float)(currentTime % 60d));
        float milliseconds = (float)(currentTime % 1d * 1000d);

        return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    public void SetTimerActivity(bool active)
    {
        timerActive = active;
    }
}
