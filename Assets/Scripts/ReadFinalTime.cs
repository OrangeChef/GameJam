using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadFinalTime : MonoBehaviour
{
    void Start()
    {
        float hours = Mathf.FloorToInt(PlayerPrefs.GetFloat("Time") / 3600f);
        float minutes = Mathf.FloorToInt(PlayerPrefs.GetFloat("Time") / 60f);
        float seconds = Mathf.FloorToInt(PlayerPrefs.GetFloat("Time") % 60f);
        float centiseconds = PlayerPrefs.GetFloat("Time") % 1f * 100f;

        GetComponent<TMP_Text>().text = string.Format("Final time: {0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, centiseconds);
    }
}
