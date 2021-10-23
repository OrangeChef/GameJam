using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    [Header("Action on Key Press")]
    public ToggleMenuOnKeyPress[] menuMappings;
    public LoadSceneOnKeyPress[] sceneMappings;

    void Update()
    {
        if (menuMappings.Length > 0)
        {
            foreach (ToggleMenuOnKeyPress tmokp in menuMappings)
            {
                if (Input.GetKeyDown(tmokp.key))
                {
                    if (tmokp.menu.isMenuOpen)
                        MenuManager.Instance.CloseMenu(tmokp.menu);
                    else
                        MenuManager.Instance.OpenMenu(tmokp.menu);
                }
            }
        }

        if (sceneMappings.Length > 0)
        {
            foreach (LoadSceneOnKeyPress lsokp in sceneMappings)
            {
                if (Input.GetKeyDown(lsokp.keyToLoad))
                    LoadScene(lsokp.sceneName);
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void SetPlayerAim(Toggle aimToggle)
    {
        SavePlayer.Instance.SaveAimOnly(aimToggle.isOn);

        if (PlayerFaceCursor.Instance)
            PlayerFaceCursor.Instance.LoadAimOnly();
    }

    public void QuitGame(int exitCode)
    {
        StartCoroutine(SavePlayer.Instance.Save(false));
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

[System.Serializable]
public class ToggleMenuOnKeyPress
{
    public Menu menu;
    public KeyCode key;
}

[System.Serializable]
public class LoadSceneOnKeyPress
{
    public string sceneName;
    public KeyCode keyToLoad;
}