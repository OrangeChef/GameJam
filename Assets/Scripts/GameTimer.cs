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

    public float currentTime;

    private TMP_Text currentTimer;
    private bool timerActive = true;

    void Start()
    {
        currentTimer = GetComponent<TMP_Text>();

        if (PlayerPrefs.HasKey("Time"))
            currentTime = PlayerPrefs.GetFloat("Time");
        else
            PlayerPrefs.SetFloat("Time", 9999f);

        timerActive = true;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;

            float hours = Mathf.FloorToInt(currentTime / 3600f);
            float minutes = Mathf.FloorToInt(currentTime / 60f);
            float seconds = Mathf.FloorToInt(currentTime % 60f);
            float centiseconds = currentTime % 1f * 100f;

            if (minutes == 60)
                minutes = 0;

            currentTimer.text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, centiseconds);
        }
    }

    public void SetTimerActivity(bool active)
    {
        timerActive = active;
    }
}
