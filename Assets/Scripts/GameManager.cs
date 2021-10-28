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

    [Header("Settings")]
    public bool invertAim;
    public float volume = 1f;
    public TMP_Text volumeText;

    [Space]
    public Toggle invertToggle;
    public Toggle particlesToggle;
    public Slider volumeSlider;

    [Space]
    public bool useParticles = true;

    [Header("Pausing")]
    public string pauseKey = "Pause";
    public Menu pauseMenu;

    [Header("Restarting")]
    public string gameSceneName = "Game";
    public string restartButton = "Restart";
    public Menu restartMenu;

    private ParticleSystem[] allParticles;
    private AudioSource[] allSources;

    void Start()
    {
        allParticles = FindObjectsOfType<ParticleSystem>();
        allSources = FindObjectsOfType<AudioSource>();
        InitializeSettings();
    }

    void Update()
    {
        if (Input.GetButtonDown(pauseKey))
            PauseGame();

        if (Input.GetButtonDown(restartButton) && SceneManager.GetActiveScene().name == gameSceneName)
            MenuManager.Instance.OpenMenu(restartMenu);
    }

    void InitializeSettings()
    {
        if (PlayerPrefs.HasKey("Invert"))
            LoadInvert();
        else
        {
            invertToggle.isOn = false;
            SaveInvert();
        }

        if (PlayerPrefs.HasKey("Particles"))
            LoadParticles();
        else
        {
            particlesToggle.isOn = true;
            SaveParticles();
        }

        if (PlayerPrefs.HasKey("Volume"))
            LoadVolume();
        else
        {
            volumeSlider.value = 0.5f;
            SaveVolume();
        }
    }

    public void SaveInvert()
    {
        PlayerPrefs.SetInt("Invert", invertToggle.isOn ? 1 : 0);

        if (PlayerManager.Instance)
            PlayerManager.Instance.SaveInvert(invertToggle);

        LoadInvert();
    }

    public void LoadInvert()
    {
        invertAim = PlayerPrefs.GetInt("Invert") == 1;

        if (invertToggle)
            invertToggle.isOn = invertAim;

        if (PlayerManager.Instance)
            PlayerManager.Instance.LoadInvert();
    }

    public void SaveParticles()
    {
        PlayerPrefs.SetInt("Particles", particlesToggle.isOn ? 1 : 0);
        LoadParticles();
    }

    public void LoadParticles()
    {
        useParticles = PlayerPrefs.GetInt("Particles") == 1;

        for (int i = 0; i < allParticles.Length; i++)
        {
            allParticles[i].gameObject.SetActive(useParticles);
        }

        if (particlesToggle)
            particlesToggle.isOn = useParticles;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        LoadVolume();
    }

    public void LoadVolume()
    {
        volume = PlayerPrefs.GetFloat("Volume");

        for (int i = 0; i < allSources.Length; i++)
        {
            allSources[i].volume = volume;
        }

        if (volumeText && volumeSlider)
        {
            volumeText.text = $"Volume: {volume:0.00}";
            volumeSlider.value = volume;
        }
    }

    public void ClearPlayerPrefs(bool excludeSettings)
    {
        if (excludeSettings)
        {
            PlayerPrefs.DeleteKey("XPos");
            PlayerPrefs.DeleteKey("YPos");
            PlayerPrefs.DeleteKey("XVel");
            PlayerPrefs.DeleteKey("YVel");
            PlayerPrefs.DeleteKey("Time");;
        }    
        else
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
        ClearPlayerPrefs(true);
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == "FinishMenu" && sceneName == gameSceneName)
            ClearPlayerPrefs(true);
        else if (sceneName == "TitleMenu" && PlayerManager.Instance)
            PlayerManager.Instance.SaveAll(false);

        SceneManager.LoadScene(sceneName);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
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
