using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance;

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

    public Toggle invertAimToggle;

    [Header("Pausing")]
    public string pauseKey = "Pause";
    public Menu pauseMenu;

    [Header("Restarting")]
    public string gameSceneName = "Game";
    public string restartButton = "Restart";
    public Menu restartMenu;

    [Header("Audio Sources")]
    public TMP_Text volumeText;
    public AudioSource[] allSources;

    void Start()
    {
        if (PlayerPrefs.HasKey("InvertAim"))
            SetAimToggle();

        if (PlayerPrefs.HasKey("Volume") && allSources.Length > 0 && volumeText)
            GetVolume();

        allSources = FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown(pauseKey))
            PauseGame();

        if (Input.GetButtonDown(restartButton) && SceneManager.GetActiveScene().name == gameSceneName)
            MenuManager.Instance.OpenMenu(restartMenu);
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void PauseGame()
    {
        if (pauseMenu.isMenuOpen)
            MenuManager.Instance.CloseMenu(pauseMenu);
        else
            MenuManager.Instance.OpenMenu(pauseMenu);
    }

    public void Restart()
    {
        ClearPlayerPrefs();
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == "TitleMenu" && PlayerManager.Instance)
            PlayerManager.Instance.SaveAll(false);
        else if (sceneName == gameSceneName && SceneManager.GetActiveScene().name == "FinishMenu")
            ClearPlayerPrefs();

        SceneManager.LoadScene(sceneName);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void SetAimToggle()
    {
        invertAimToggle.isOn = PlayerPrefs.GetInt("InvertAim") == 1;
    }

    public void SetInvertAim(Toggle toggle)
    {
        PlayerPrefs.SetInt("InvertAim", toggle.isOn ? 1 : 0);

        if (PlayerManager.Instance)
            PlayerManager.Instance.SetInvertAim(toggle);
    }

    public void GetVolume()
    {
        for (int i = 0; i < allSources.Length; i++)
        {
            allSources[i].volume = PlayerPrefs.GetFloat("Volume");
        }

        volumeText.text = $"Volume: {PlayerPrefs.GetFloat("Volume"):0.00}";
    }

    public void SetVolume(Slider s)
    {
        for (int i = 0; i < allSources.Length; i++)
        {
            allSources[i].volume = s.value;
        }
        
        volumeText.text = $"Volume: {s.value:0.00}";
        PlayerPrefs.SetFloat("Volume", s.value);
    }

    public void QuitGame(int exitCode)
    {
        if (PlayerManager.Instance)
            PlayerManager.Instance.SaveAll(false);

#if UNITY_EDITOR
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }
#endif
        Application.Quit(exitCode);
    }
}
