using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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

    public void QuitGame(int exitCode)
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }

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