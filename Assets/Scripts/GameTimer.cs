using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{

    private TMP_Text currentTimer;
    private float currentTime;

    void Start()
    {
        currentTimer = GetComponent<TMP_Text>();
        currentTime = 0f;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        float hours = Mathf.FloorToInt(currentTime / 3600f);
        float minutes = Mathf.FloorToInt(currentTime / 60f);
        float seconds = Mathf.FloorToInt(currentTime % 60f);
        float centiseconds = currentTime % 1f * 100f;

        if (minutes == 60)
            minutes = 0;

        currentTimer.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", hours, minutes, seconds, centiseconds);
    }
}
