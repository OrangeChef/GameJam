using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static int checkpoint;
    public Transform[] checkpointTransforms;

    [Header("Action on Key Press")]
    public ToggleMenuOnKeyPress[] menuMappings;
    public LoadSceneOnKeyPress[] sceneMappings;

    private void Start()
    {
        PutPlayerAtCheckpoint(checkpoint);
    }

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
        GameObject.FindWithTag("Player").transform.position = checkpointTransforms[checkpoint].position;
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void QuitGame(int exitCode)
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }
#endif
        Application.Quit(exitCode);
    }

    public void SetCheckpoint(int cp)
    {
        checkpoint = cp;
    }

    public void PutPlayerAtCheckpoint(int cp)
    {
        GameObject.FindWithTag("Player").transform.position = checkpointTransforms[cp].position;
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